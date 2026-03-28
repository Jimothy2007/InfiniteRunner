using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 7f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool jumpPressed;
    [SerializeField] private bool isGamePaused = false;
    [SerializeField] private int health = 3;
    
    private float iFramesDuration = 1f;
    private float iFramesTimer = 0f;
    private Rigidbody2D rb;
    private UnityEngine.InputSystem.PlayerInput controls;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (controls == null)
        {
            controls = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        }
    }

    void FixedUpdate()
    {
        if (isGrounded && jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            isGrounded = false;
        }

        if (!jumpPressed && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (iFramesTimer > 0)
        {
            iFramesTimer -= Time.fixedDeltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void Update()
    {
        if (health <= 0 || (transform.position.y <= -6))
        {
            GameManager.instance.Death();
        }
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if ((collision.gameObject.CompareTag("Obstacle")) && (iFramesTimer <= 0))
        {
            takeDamage(1);
            iFramesTimer = iFramesDuration;
        }
    }

    void OnJump(InputValue value)
    {
        Debug.Log("Jump Pressed");
        jumpPressed = value.isPressed;
    }

    void OnPauseGame(InputValue value)
    {
        if (value.isPressed && !isGamePaused)
        {
            isGamePaused = true;
            Debug.Log("Pause Pressed");
            GameManager.instance.PauseGame();
        }
        else if (value.isPressed && isGamePaused)
        {
            isGamePaused = false;
            Debug.Log("Resume Pressed");
            GameManager.instance.ResumeGame();
        }
    }

    void takeDamage(int damage)
    {
        Debug.Log("Player took damage: " + damage);
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Player has died.");
            GameManager.instance.Death();
        }
    }
}
