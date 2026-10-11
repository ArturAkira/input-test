using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class npcManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col;
    [SerializeField] public GameObject textCanvas;
    public TMP_Text textBox;

    public bool placeholder;
    private int count = 0;
    
    public string[] texts;

    private void Start()
    {
        textCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") == true)
        {
            placeholder = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") == true)
        {
            placeholder = false;
        }
    }
    public void OnInteract(InputValue value)
    {
            if (placeholder == true) 
            {
                textCanvas.SetActive(true);
                gameManager.Instance.dialogTime = true;
                dialogManager(texts);
            }
    }

    private void dialogManager(string[] texts)
    {
        if(count < texts.Length && texts != null)
        {
            textBox.text = texts[count];
        }
        else
        {
            gameManager.Instance.dialogTime = false;
            count = 0;
            textCanvas.SetActive(false);
            return;
        }
        count++;
    }
}
