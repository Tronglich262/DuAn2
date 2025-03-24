using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;
    private Vector2 inputVector;

    public Vector2 GetInput() => inputVector;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position - (Vector2)background.position;
        float radius = background.sizeDelta.x / 2;
        inputVector = (pos.magnitude > radius) ? pos.normalized : pos / radius;
        handle.anchoredPosition = inputVector * radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}