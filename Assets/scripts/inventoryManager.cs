using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class inventoryManager : MonoBehaviour
{
    public GameObject invent;
    void Start()
    {
        invent = GameObject.Find("inventario");
        invent.SetActive(false);
    }

    private void Update()
    {
        if (invent.activeSelf == true)
        {
            
        }

    }

    public void OnInventory(InputValue value)
    {
        Debug.Log(value);
        invent.SetActive(!invent.activeSelf);
    }
}
