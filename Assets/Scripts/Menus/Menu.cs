using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
	public float MenuWidth;
	public float MenuHeight;
	
	private RectTransform Scroller;
	private RectTransform Mask;
	private RectTransform MenuTransform;
	private Camera SceneCamera;
	
	private const float EXPAND_TIME = 0.25f;
	private bool Active = false;
	
	void Start()
	{
		MenuTransform = gameObject.GetComponent<RectTransform>();
		Mask = transform.Find("Mask").GetComponent<RectTransform>();
		Scroller = Mask.transform.Find("Scroll View").GetComponent<RectTransform>();
		SceneCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}
	
    public void Activate()
	{
		if (Active == false)
		{
			Active = true;
			//Mask.gameObject.SetActive(true);
			StartCoroutine(Expand());
		}
	}
	
	private IEnumerator Expand()
	{
		Vector2 size = Mask.sizeDelta;
		for (float elapsedTime = 0f; elapsedTime < EXPAND_TIME; elapsedTime += Time.deltaTime)
		{
			size.y = Mathf.Lerp(0f, MenuHeight, elapsedTime / EXPAND_TIME);
			size.x = Mathf.Lerp(0f, MenuWidth, elapsedTime / EXPAND_TIME);
			Mask.sizeDelta = size;
			
			Scroller.sizeDelta = Mask.sizeDelta;
			
			yield return null;
		}
		
		size.y = MenuHeight;
		size.x = MenuWidth;
		Mask.sizeDelta = size;
		Scroller.sizeDelta = Mask.sizeDelta;
	}
	
	public void Deactivate()
	{
		if (Active == true)
		{
			StartCoroutine(Collapse());
		}
	}
	
	private IEnumerator Collapse()
	{
		Vector2 size = Mask.sizeDelta;
		for (float elapsedTime = 0f; elapsedTime < EXPAND_TIME; elapsedTime += Time.deltaTime)
		{
			size.y = Mathf.Lerp(MenuHeight, 0f, elapsedTime / EXPAND_TIME);
			size.x = Mathf.Lerp(MenuWidth, 0f, elapsedTime / EXPAND_TIME);
			Mask.sizeDelta = size;
			
			yield return null;
		}
		
		size.y = 0f;
		size.x = 0f;
		Mask.sizeDelta = size;
		
		
		Active = false;
		//Mask.gameObject.SetActive(false);
	}
	
	/*public void StartDrag()
	{
		//StartCoroutine(Drag());
		Debug.Log("start");
	}
	
	public void EndDrag()
	{
		//StopCoroutine(Drag());
		Debug.Log("end");
	}
	
	private IEnumerator Drag()
	{
		yield return null;
	}*/
	
	private bool IsDragging = false;
	private Vector2 DragOffset = new Vector2();
	private Vector2 SavedMousePos;
	
	public void StartDrag()
    {
        IsDragging = true;
		SavedMousePos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MenuTransform, SavedMousePos, SceneCamera, out DragOffset);
    }

    public void EndDrag()
    {
        IsDragging = false;
    }

    public void OnDrag()
    {
        if (!IsDragging)
            return;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MenuTransform, mousePosition, SceneCamera, out localPosition);
        MenuTransform.localPosition = localPosition - DragOffset;
		Debug.Log(mousePosition);
    }
}
