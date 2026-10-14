using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float detectionDistance = 5f;
    public LayerMask playerLayer;
    public float iniPosX, lastPosX, iniPosY, lastPosY;
    public bool turn;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
    private bool isAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 direction = new (player.position.x - transform.position.x, 0);

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            detectionDistance,
            playerLayer
        );
        if (turn) {rb.transform.localScale = new Vector2(-1, 1);}
        else { rb.transform.localScale = new Vector2(1, 1); }
        if (!isAttacking)
        {
            if (hit.collider != null && hit.collider.CompareTag("Player") && direction.x > 0)
            {
                rb.linearVelocityX = 1 * speed;
                turn = true;
            }
            else if (hit.collider != null && hit.collider.CompareTag("Player") && direction.x < 0)
            {
                rb.linearVelocityX = -1 * speed;
                turn = false;
            }
            else
            {
                cicleWalk(new(iniPosX, iniPosY), new(lastPosX, lastPosY));
            }
        }
        StartCoroutine(enemyAttack(direction.x));
    }

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            Gizmos.DrawRay(
                transform.position,
                direction * detectionDistance
            );
        }
    }

    private void cicleWalk(Vector2 coordIni, Vector2 coordLast)
    {
        if (turn == false)
        {
            if (transform.position.x >= coordIni.x) { rb.linearVelocityX = -1 * speed; }
            else { turn = true; }
        }
        if (turn == true)
        {
            if (transform.position.x <= coordLast.x) { rb.linearVelocityX = 1 * speed; }
            else { turn = false; }
        }
    }

    IEnumerator enemyAttack(float direction)
    {
        if (isAttacking) { yield break; }

        if (direction <= 2 && direction >= -2)
        {
            isAttacking = true;
            rb.linearVelocityX = 0;
            anim.Play("enemyAttack1");
            yield return new WaitForSeconds(1.5f);

            //if (direction <= 2 && direction >= -2) { yield return new WaitForSeconds(1f); }
                
            isAttacking = false;
        }
    }
}
