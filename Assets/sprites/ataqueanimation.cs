using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ataqueanimation : MonoBehaviour
{
    private Animator anim;
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
        Debug.Log("oi");
    }
}
