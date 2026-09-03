using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ataqueanimation : MonoBehaviour
{
    private Animator anim;
    public GameObject attacPoint;
    public float radius;
    public LayerMask enemies;
    public float playerDamage = 10;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
      
    }

    public void OnAtaque(InputAction.CallbackContext context)
    {
        
        if (context.started)
        {
            anim.SetBool("botao", true);
        }
        if (context.canceled)
        {
            anim.SetBool("botao", false);
        }
    }

    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attacPoint.transform.position, radius, enemies);
        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("hit confirmed");
            enemyGameobject.GetComponent<enemy>().enemyHealth -= playerDamage;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attacPoint.transform.position, radius);
    }
}
