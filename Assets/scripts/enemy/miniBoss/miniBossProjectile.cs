
using UnityEngine;
using System;

public class miniBossProjectile : MonoBehaviour
{
    static float test(int value1, int value2)
    {
        System.Random rand = new System.Random();

        float x = rand.Next(value1, value2) / 10f;

        return x;
    }

    public int side;

    Vector3 angle = new Vector3(
        test(100, 2000),
        test(5000, 10000),
        0
    );

    Rigidbody2D rb;
    PolygonCollider2D pcol;

    void Start()
    {
        transform.localScale = new Vector3(0.25f, 0.25f, 1f);

        rb = GetComponent<Rigidbody2D>();
        pcol = GetComponent<PolygonCollider2D>();

        rb.AddForce(new Vector2(angle.x * side, angle.y));
    }

    void Update()
    {
        Vector2 direction = rb.linearVelocity;

        if (direction.sqrMagnitude > 0.01f)
        {
            float rotationAngle = Mathf.Atan2(
                direction.y,
                direction.x
            ) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(
                0f, 0f, rotationAngle
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ground") ||
            other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            gameManager.Instance.playerHp -= 10;
            Destroy(gameObject);
        }
    }
}
