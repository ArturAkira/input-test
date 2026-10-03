using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class playermanager : MonoBehaviour
{
    [SerializeField] private Transform atkPoint;

    private Rigidbody2D rb;
    private BoxCollider2D pcol;
    private float movement;
    private bool isGrounded;
    private bool isWall = false;
    private int direction;
    private int oldDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
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
        if (col.gameObject.CompareTag("wall") == true)
        {
            oldDirection = direction;
            isWall = true;
        }
    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("wall") == true)
        {
            isWall = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        float input = 1;
        if (context.performed)
        {
            if (isGrounded == true)
            {
                rb.linearVelocityY = input * 17f;
            }

            else if (isGrounded == false && isWall == true && oldDirection == direction)
            {
                    rb.linearVelocityY = input * 15f;
                    movement *= -1f;
                    StartCoroutine(wait());
                isWall = false;
            }
            isGrounded = false;
        }
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            direction = (input.x > 0) ? 1 : -1;
            movement = input.x * 7f;
            rb.transform.localScale = new Vector2(direction, 1);
        }
        if (context.canceled)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movement = input.x * 7f;
        }
        
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.15f);
        movement = direction *5f;
    }
}
