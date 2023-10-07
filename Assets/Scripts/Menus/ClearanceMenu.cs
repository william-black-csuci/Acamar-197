using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearanceMenu : Menu
{
	private RectTransform Scroller;
	private Scrollbar Scrollbar;
	
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		
		Scroller = Mask.transform.Find("Scroll View").GetComponent<RectTransform>();
		Scrollbar = Mask.transform.Find("Scroll View").Find("Scrollbar Vertical").GetComponent<Scrollbar>();
    }

    public override void Activate()
	{
		base.Activate();
		
		Scrollbar.value = 1f;
		StartCoroutine(ExpandScroller());
	}
	
	private IEnumerator ExpandScroller()
	{
		Vector2 size = Mask.sizeDelta;
		for (float elapsedTime = 0f; elapsedTime < EXPAND_TIME; elapsedTime += Time.deltaTime)
		{
			size.y = Mathf.Lerp(0f, MenuHeight, elapsedTime / EXPAND_TIME);
			size.x = Mathf.Lerp(0f, MenuWidth, elapsedTime / EXPAND_TIME);
			
			Scroller.sizeDelta = size;
			
			yield return null;
		}
		
		size.y = MenuHeight;
		size.x = MenuWidth;
		Scroller.sizeDelta = size;
	}
}
