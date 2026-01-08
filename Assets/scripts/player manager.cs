using UnityEngine;
using UnityEngine.InputSystem;

public class playermanager : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D pcol;
    private float movement;
    private bool isGrounded;
    
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
        if (GameObject.FindGameObjectWithTag("ground") == true)
        {
            isGrounded = true;
        }
    }
    public void OnJump(InputValue Value)
    {
        float input = Value.Get<float>();
        if (isGrounded == true)
        {
            rb.linearVelocityY = input * 25f;
            isGrounded = false;
        }
        
    }
    public void OnMove(InputValue Value)
    {
        Vector2 input = Value.Get<Vector2>();
        movement = input.x * 10f;
    }
}
