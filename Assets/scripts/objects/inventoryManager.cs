using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class inventoryManager : MonoBehaviour
{
    public GameObject invent;
    public TMP_InputField inputField;
    public bool closeInvent = false;
    void Start()
    {
        //coloca o texto
        inputField = GetComponentInChildren<TMP_InputField>();
        invent = GameObject.Find("inventario");
        invent.SetActive(false);
        gameManager.Instance.setContent(inputField);
    }

    private void Update()
    {
        if (invent.activeSelf == true)
        {
            gameManager.Instance.stopPlayer = true;
        }
        else
        {
            gameManager.Instance.stopPlayer = false;
        }

            if (closeInvent == true)
        {
            invent.SetActive(!invent.activeSelf);
            closeInvent = false;
        }
    }

    public void OnInventory(InputValue value)
    {
        if (invent.activeSelf == false) invent.SetActive(!invent.activeSelf);
    }
}