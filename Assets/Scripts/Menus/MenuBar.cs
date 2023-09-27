using UnityEngine;
using UnityEngine.EventSystems;

public class MenuBar : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform _parentRectTransform;
	
    private void Start()
    {
        _parentRectTransform = transform.parent.parent.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the UI element's position based on the mouse drag
		_parentRectTransform.localPosition += (Vector3)eventData.delta;// / 0.9675f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
