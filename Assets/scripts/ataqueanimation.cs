using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class ataqueanimation : MonoBehaviour
{
    [SerializeField] private GameObject attacPoint;

    public RuntimeAnimatorController normalController;
    public AnimatorOverrideController knifeController;

    private Animator anim;
    public float radius;
    public LayerMask enemies;
    public float playerDamage = 10;
    private string _playerWeponId = "0";

    public string playerWeponId
    {
        get { return _playerWeponId; }
        set
        {
            if (_playerWeponId != value)
            {
                _playerWeponId = value;
                Debug.Log("kdjbavdb");
                switch (_playerWeponId)
                {
                    case null:
                        NoWepon();
                        Debug.Log("está null");
                        break;
                    case "0":
                        NoWepon();
                        break;
                    case "1":
                        AnimKnife_1();
                        break;
                }
            }
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (gameManager.Instance.equipSlots[3] == null) { playerWeponId = "0"; }
        else {playerWeponId = gameManager.Instance.equipSlots[3].itemId;
        }

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

    private void AnimKnife_1()
    {
        anim.runtimeAnimatorController = knifeController;
    }

    private void NoWepon()
    {
        anim.runtimeAnimatorController = normalController;
    }
}
