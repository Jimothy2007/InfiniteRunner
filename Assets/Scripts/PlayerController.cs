using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float jumpHeight = 5f;
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
            Debug.Log("Jumping");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            jumpPressed = false;
        }
    }

    void OnJump(InputValue value)
    {
        jumpPressed = value.isPressed;
    }
}
