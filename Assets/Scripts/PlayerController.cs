using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float jumpHeight = 7f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool jumpPressed;

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

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnJump(InputValue value)
    {
        Debug.Log("Jump Pressed");
        jumpPressed = value.isPressed;
    }
}
