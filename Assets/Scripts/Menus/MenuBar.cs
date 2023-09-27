using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuBar : EventTrigger//MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform _parentRectTransform;
	/*
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
		_parentRectTransform.anchoredPosition += eventData.delta / 0.9675f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }*/

    private bool dragging;
	
    private Vector2 offset;

    public void Update() {
        if (dragging) {
            _parentRectTransform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - offset;
        }
    }

	private void Start()
    {
        _parentRectTransform = transform.parent.parent.GetComponent<RectTransform>();
    }

    public override void OnPointerDown(PointerEventData eventData) {
        dragging = true;
		offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        dragging = false;
    }
}
