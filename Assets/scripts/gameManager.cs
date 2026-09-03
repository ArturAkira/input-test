using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{ 
    public static gameManager Instance;
    public string text;

    public Image[] slotImage;
    public Objects[] slots;
    public int[] slotAmount;
    public Image[] slotItemImage;

    public Image[] equipSlotImage;
    public Objects[] equipSlots;
    public int[] equipSlotAmount;
    public Image[] equipItemImage;

    public HashSet<string> collectedItems = new HashSet<string>();

    public bool stopPlayer;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else {Destroy(gameObject); }

        slots = new Objects[slotImage.Length];
        slotAmount = new int[slotImage.Length];

        equipSlots = new Objects[equipSlotImage.Length];
        equipSlotAmount = new int[equipSlotImage.Length];
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (stopPlayer == true)
            {Time.timeScale = 0f;}
        else
            { Time.timeScale = 1f;}
    }

    public void saveContent(TMP_InputField inputField)
    {
        text = inputField.text;
        Debug.Log(text);
    }
    public void setContent(TMP_InputField inputField)
    {
        if (text != null && text != "") { inputField.text = text; }
    }
}
