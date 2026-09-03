using System.Diagnostics;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemyHealth = 30;
    void Update()
    {
        if (enemyHealth <= 0)
        {
            UnityEngine.Debug.Log("morreu");
            Destroy(gameObject);
        }
    }
}
