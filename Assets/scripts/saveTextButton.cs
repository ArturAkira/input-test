using UnityEngine;
using UnityEngine.UI;

public class saveTextButton : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exit;
    [SerializeField] private inventoryManager inventoryManager;
    void Start()
    {
        if (inventoryManager == null) { inventoryManager = Object.FindFirstObjectByType<inventoryManager>(); }
        saveButton.onClick.AddListener(saveButtonClick);
        exit.onClick.AddListener(exitButtonClick);
    }

    void saveButtonClick()
    {
        gameManager.Instance.saveContent(inventoryManager.inputField);
    }

    void exitButtonClick()
    {
        inventoryManager.closeInvent = true;
    }
}
