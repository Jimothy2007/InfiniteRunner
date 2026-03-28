using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 7f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool jumpPressed;
    [SerializeField] private int health = 3;
    [SerializeField] private bool isAlive = true;

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
    }

    private void Update()
    {
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            Debug.Log("Player has died.");
        }
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            takeDamage(1);
        }
    }

    void OnJump(InputValue value)
    {
        Debug.Log("Jump Pressed");
        jumpPressed = value.isPressed;
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
