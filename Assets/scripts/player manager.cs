using UnityEngine;
using UnityEngine.InputSystem;

public class playermanager : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D pcol;
    private float movement;
    public bool isGrounded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = movement;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground") == true)
        {
            isGrounded = true;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        float input = 1;
        if (isGrounded == true)
        {
            rb.linearVelocityY = input * 25f;
            isGrounded = false;
        }
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movement = input.x * 10f;
        }
        if (context.canceled)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movement = input.x * 10f;
        }
        
    }
}
