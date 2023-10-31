using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Canvas canvas; // The main canvas UI

    public void Init(Canvas canvas)
    {
        this.canvas = canvas;
        this.canvasGroup = canvas.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform); // Set the parent to canvas so that the item isn't clipped by other UI elements
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas.transform)
        {
            transform.SetParent(originalParent); // If not dropped on a valid slot, return to original position
        }
        canvasGroup.blocksRaycasts = true;
    }

    public void AttachToSlot(ItemSlot slot)
    {
        transform.SetParent(slot.transform);
        transform.position = slot.transform.position; // Snap to the slot's position
        slot.currentItem = this;
    }
}
