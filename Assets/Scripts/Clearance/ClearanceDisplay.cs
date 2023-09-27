using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearanceDisplay : MonoBehaviour
{
	private ClearanceGenerator Clearance;
	private TMP_Text TextTemplate;
	private RectTransform TextTransform;
	private RectTransform Content;
	
    // Start is called before the first frame update
    void Start()
    {
        Clearance = GameObject.FindWithTag("Clearance Generator").GetComponent<ClearanceGenerator>();
		TextTemplate = transform.Find("Text Template").GetComponent<TMP_Text>();
		TextTransform = TextTemplate.gameObject.GetComponent<RectTransform>();
		Content = gameObject.GetComponent<RectTransform>();
		
		Vector2 contentSize = Content.sizeDelta;
		GameObject codeObject;
		Vector2 textAnchor = TextTransform.anchoredPosition;
		foreach(string code in Clearance.Codes)
		{
			TextTemplate.text = code;
			
			codeObject = Instantiate(TextTemplate.gameObject, transform);
			codeObject.SetActive(true);
			
			textAnchor.y -= 25f;
			TextTransform.anchoredPosition = textAnchor;
			
			contentSize.y += 25f;
		}
		
		Content.sizeDelta = contentSize;
    }
}
