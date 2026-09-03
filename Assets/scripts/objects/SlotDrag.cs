using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int slotIndex;      // índice deste slot (configurar no Inspector ou via script)
    public bool isEquipSlot;   // marcar true se for um slot de equipamento

    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Image image;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!image.enabled) return; // não arrasta slot vazio

        originalParent = transform.parent;
        transform.SetParent(transform.root); // joga pro topo da hierarquia (acima de tudo)
        canvasGroup.blocksRaycasts = false;   // deixa o mouse "atravessar" pra detectar o slot embaixo
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        SlotDrag draggedSlot = eventData.pointerDrag.GetComponent<SlotDrag>();
        if (draggedSlot == null) return;

        inventoryController inv = FindObjectOfType<inventoryController>();

        if (!isEquipSlot && !draggedSlot.isEquipSlot)
        {
            inv.MoveItem(draggedSlot.slotIndex, slotIndex);
        }
        else if (isEquipSlot && !draggedSlot.isEquipSlot)
        {
            inv.EquipItem(draggedSlot.slotIndex, slotIndex);
        }
        else if (!isEquipSlot && draggedSlot.isEquipSlot)
        {
            inv.UnequipItem(draggedSlot.slotIndex, slotIndex);
        }
        // (você pode adicionar aqui o caso de desequipar, arrastando do equip pro inventário)
    }
}