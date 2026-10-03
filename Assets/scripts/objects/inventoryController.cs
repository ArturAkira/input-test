using UnityEngine;

public class inventoryController : MonoBehaviour
{
    [SerializeField] private ataqueanimation ataqueanimation;
    void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        // ==========================================
        // SLOTS NORMAIS
        // ==========================================

        for (int i = 0; i < gameManager.Instance.slotImage.Length; i++)
        {
            if (gameManager.Instance.slots[i] != null)
            {
                // Mostra o sprite do item
                gameManager.Instance.slotItemImage[i].sprite =
                    gameManager.Instance.slots[i].itemSprite;

                gameManager.Instance.slotItemImage[i].enabled = true;
            }
            else
            {
                // Esconde somente o item
                // O fundo do slot continua aparecendo
                gameManager.Instance.slotItemImage[i].sprite = null;

                gameManager.Instance.slotItemImage[i].enabled = false;
            }
        }


        // ==========================================
        // SLOTS DE EQUIPAMENTO
        // ==========================================

        for (int i = 0; i < gameManager.Instance.equipSlotImage.Length; i++)
        {
            if (gameManager.Instance.equipSlots[i] != null)
            {
                gameManager.Instance.equipItemImage[i].sprite =
                    gameManager.Instance.equipSlots[i].itemSprite;

                gameManager.Instance.equipItemImage[i].enabled = true;
            }
            else
            {
                // Esconde somente o item
                // O fundo do slot continua aparecendo
                gameManager.Instance.equipItemImage[i].sprite = null;

                gameManager.Instance.equipItemImage[i].enabled = false;
            }
        }
    }


    // =========================================================
    // PEGAR ITEM DO CHÃO
    // =========================================================

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("item"))
            return;


        itemType item = col.GetComponent<itemType>();

        if (item == null)
            return;


        // ==========================================
        // PROCURA UM SLOT PARA O ITEM
        // ==========================================

        for (int i = 0; i < gameManager.Instance.slots.Length; i++)
        {
            // ======================================
            // SLOT VAZIO
            // ======================================

            if (gameManager.Instance.slots[i] == null)
            {
                gameManager.Instance.slots[i] =
                    item.objectType;

                gameManager.Instance.slotAmount[i] = 1;

                gameManager.Instance.collectedItems.Add(
                    item.objectType.itemId
                );

                Destroy(col.gameObject);

                UpdateUI();

                break;
            }


            // ======================================
            // MESMO ITEM → EMPILHA
            // ======================================

            else if (gameManager.Instance.slots[i] == item.objectType)
            {
                gameManager.Instance.slotAmount[i]++;

                gameManager.Instance.collectedItems.Add(
                    item.objectType.itemId
                );

                Destroy(col.gameObject);

                UpdateUI();

                break;
            }
        }
    }


    // =========================================================
    // MOVER ITEM ENTRE SLOTS NORMAIS
    // =========================================================

    public void MoveItem(int originalSlot, int targetSlot)
    {
        // Verifica se os índices são válidos
        if (originalSlot < 0 ||
            originalSlot >= gameManager.Instance.slots.Length)
            return;

        if (targetSlot < 0 ||
            targetSlot >= gameManager.Instance.slots.Length)
            return;


        // Não faz nada se for o mesmo slot
        if (originalSlot == targetSlot)
            return;


        // Verifica se existe item
        if (gameManager.Instance.slots[originalSlot] == null)
            return;


        Objects item =
            gameManager.Instance.slots[originalSlot];


        // ==========================================
        // DESTINO VAZIO
        // ==========================================

        if (gameManager.Instance.slots[targetSlot] == null)
        {
            gameManager.Instance.slots[targetSlot] =
                gameManager.Instance.slots[originalSlot];

            gameManager.Instance.slotAmount[targetSlot] =
                gameManager.Instance.slotAmount[originalSlot];

            gameManager.Instance.slots[originalSlot] = null;

            gameManager.Instance.slotAmount[originalSlot] = 0;
        }


        // ==========================================
        // MESMO ITEM → EMPILHA
        // ==========================================

        else if (gameManager.Instance.slots[targetSlot] == item)
        {
            gameManager.Instance.slotAmount[targetSlot] +=
                gameManager.Instance.slotAmount[originalSlot];

            gameManager.Instance.slots[originalSlot] = null;

            gameManager.Instance.slotAmount[originalSlot] = 0;
        }


        // ==========================================
        // ITEM DIFERENTE → TROCA
        // ==========================================

        else
        {
            Objects tempObject =
                gameManager.Instance.slots[targetSlot];

            int tempAmount =
                gameManager.Instance.slotAmount[targetSlot];


            gameManager.Instance.slots[targetSlot] =
                gameManager.Instance.slots[originalSlot];

            gameManager.Instance.slotAmount[targetSlot] =
                gameManager.Instance.slotAmount[originalSlot];


            gameManager.Instance.slots[originalSlot] =
                tempObject;

            gameManager.Instance.slotAmount[originalSlot] =
                tempAmount;
        }


        UpdateUI();
    }


    // =========================================================
    // EQUIPAR ITEM
    // =========================================================

    public bool EquipItem(int inventorySlot, int equipSlot)
    {
        // ==========================================
        // VERIFICA OS ÍNDICES
        // ==========================================

        if (inventorySlot < 0 ||
            inventorySlot >= gameManager.Instance.slots.Length)
            return false;

        if (equipSlot < 0 ||
            equipSlot >= gameManager.Instance.equipSlots.Length)
            return false;


        // ==========================================
        // PEGA O ITEM DO INVENTÁRIO
        // ==========================================

        Objects item =
            gameManager.Instance.slots[inventorySlot];


        if (item == null)
            return false;


        // =====================================================
        // VERIFICAÇÃO DO TIPO DE ITEM
        // =====================================================

        bool podeEquipar = false;


        switch (equipSlot)
        {
            // ==============================================
            // [0] HEAD
            // ==============================================

            case 0:

                if (item.itemClass == "head")
                {
                    podeEquipar = true;
                }

                break;


            // ==============================================
            // [1] BODY
            // ==============================================

            case 1:

                if (item.itemClass == "body")
                {
                    podeEquipar = true;
                }

                break;


            // ==============================================
            // [2] LEGS
            // ==============================================

            case 2:

                if (item.itemClass == "legs")
                {
                    podeEquipar = true;
                }

                break;


            // ==============================================
            // [3] WEPON
            // ==============================================

            case 3:

                if (item.itemClass == "wepon")
                {
                    podeEquipar = true;
                }

                break;


            // ==============================================
            // [4] ITEM
            // ==============================================

            case 4:

                if (item.itemClass == "item")
                {
                    podeEquipar = true;
                }

                break;
        }


        // =====================================================
        // ITEM NÃO PODE SER COLOCADO NESSE SLOT
        // =====================================================

        if (!podeEquipar)
        {
            Debug.Log(
                "O item " + item.itemName +
                " não pode ser colocado no slot " +
                equipSlot
            );

            return false;
        }


        // =====================================================
        // SLOT DE EQUIPAMENTO VAZIO
        // =====================================================

        if (gameManager.Instance.equipSlots[equipSlot] == null)
        {
            gameManager.Instance.equipSlots[equipSlot] =
                item;

            gameManager.Instance.equipSlotAmount[equipSlot] =
                gameManager.Instance.slotAmount[inventorySlot];


            // Remove do inventário
            gameManager.Instance.slots[inventorySlot] = null;

            gameManager.Instance.slotAmount[inventorySlot] = 0;
        }


        // =====================================================
        // SLOT DE EQUIPAMENTO OCUPADO
        // =====================================================

        else
        {
            Objects oldItem =
                gameManager.Instance.equipSlots[equipSlot];

            int oldAmount =
                gameManager.Instance.equipSlotAmount[equipSlot];


            // Coloca o novo item no equipamento
            gameManager.Instance.equipSlots[equipSlot] =
                item;

            gameManager.Instance.equipSlotAmount[equipSlot] =
                gameManager.Instance.slotAmount[inventorySlot];


            // Coloca o equipamento antigo no inventário
            gameManager.Instance.slots[inventorySlot] =
                oldItem;

            gameManager.Instance.slotAmount[inventorySlot] =
                oldAmount;
        }


        UpdateUI();

        return true;
    }

    // =========================================================
    // DESEQUIPAR ITEM
    // =========================================================

    public bool UnequipItem(int equipSlot, int inventorySlot)
    {
        // ==========================================
        // VERIFICA OS ÍNDICES
        // ==========================================

        if (equipSlot < 0 ||
            equipSlot >= gameManager.Instance.equipSlots.Length)
            return false;

        if (inventorySlot < 0 ||
            inventorySlot >= gameManager.Instance.slots.Length)
            return false;


        Objects item =
            gameManager.Instance.equipSlots[equipSlot];


        if (item == null)
            return false;


        // =====================================================
        // SLOT DE INVENTÁRIO VAZIO
        // =====================================================

        if (gameManager.Instance.slots[inventorySlot] == null)
        {
            gameManager.Instance.slots[inventorySlot] =
                item;

            gameManager.Instance.slotAmount[inventorySlot] =
                gameManager.Instance.equipSlotAmount[equipSlot];


            // Remove do equipamento
            gameManager.Instance.equipSlots[equipSlot] = null;
            gameManager.Instance.equipSlotAmount[equipSlot] = 0;
        }


        // =====================================================
        // SLOT DE INVENTÁRIO OCUPADO → TROCA
        // =====================================================

        else
        {
            Objects oldItem =
                gameManager.Instance.slots[inventorySlot];

            int oldAmount =
                gameManager.Instance.slotAmount[inventorySlot];


            // Coloca o item equipado no inventário
            gameManager.Instance.slots[inventorySlot] =
                item;

            gameManager.Instance.slotAmount[inventorySlot] =
                gameManager.Instance.equipSlotAmount[equipSlot];


            // Coloca o item antigo do inventário no slot de equipamento
            // (só faz sentido se o item antigo puder ser equipado ali,
            // então aqui optamos por só liberar o slot de equipamento)
            gameManager.Instance.equipSlots[equipSlot] = null;
            gameManager.Instance.equipSlotAmount[equipSlot] = 0;

            // Se preferir trocar os dois de lugar mesmo sem checar o tipo, use:
            // gameManager.Instance.equipSlots[equipSlot] = oldItem;
            // gameManager.Instance.equipSlotAmount[equipSlot] = oldAmount;
        }


        UpdateUI();

        return true;
    }
}