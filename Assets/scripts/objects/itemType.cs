using System.IO;
using UnityEngine;

public class itemType : MonoBehaviour
{
    public Objects objectType;

    void Start()
    {
        if (gameManager.Instance.collectedItems.Contains(objectType.itemId))
        {
            Destroy(gameObject);
        }
    }
}
