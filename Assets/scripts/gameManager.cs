using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{ 
    public static gameManager Instance;

    //diary text
    public string text;

    //slots and itens
    public Objects[] slots;
    public int[] slotAmount;

    public Objects[] equipSlots;
    public int[] equipSlotAmount;

    public HashSet<string> collectedItems = new HashSet<string>();

    //player stats
    public bool stopPlayer;

    public float playerMaxHp;
    public float playerHp;
    public bool isStarting = true;
    private bool isDead = false;

    public float coordX = 0;
    public float coordY = 0;

    //textbox
    
    public bool dialogTime;

    private void Awake()
    {
        if (Instance == null) { 
            Instance = this; 
            DontDestroyOnLoad(gameObject);

            slots = new Objects[15];
            slotAmount = new int[15];

            equipSlots = new Objects[5];
            equipSlotAmount = new int[5];
        }
        else {
            Destroy(gameObject); 
        }
        playerMaxHp = (playerMaxHp == 0) ? 50 : playerMaxHp;
        playerHp = (isStarting == true) ? playerHp = playerMaxHp : playerHp;
    }
    void Start()
    {
    }

    void Update()
    {
        if (stopPlayer == true || isDead == true || dialogTime == true)
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
    public void deathScreen()
    {
        isDead = true; Time.timeScale = 0f; ; Debug.Log("Voce morreu");
    }
}
