using System.Diagnostics;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemyHealth = 30;
    public float enemyAttack = 10;
    void Update()
    {
        if (enemyHealth <= 0)
        {
            UnityEngine.Debug.Log("morreu");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player") == true)
        {
            gameManager.Instance.playerHp -= enemyAttack;
            if (gameManager.Instance.playerHp <= 0) gameManager.Instance.deathScreen();
        }
    }
}
