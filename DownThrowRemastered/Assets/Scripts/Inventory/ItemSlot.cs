using UnityEngine;
using UnityEngine.EventSystems; // For UI events

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector]
    public DraggableItem currentItem;

    public void SetItem(DraggableItem draggableItem)
    {
        this.currentItem = draggableItem;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (draggableItem != null)
            {
                // Check if the slot is empty or any other conditions you might have
                if (currentItem == null)
                {
                    draggableItem.AttachToSlot(this);
                }
            }
        }
    }
}