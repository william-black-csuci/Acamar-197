using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DialoguePrinter : MonoBehaviour
{
	public GameObject RemoteBubblePrefab;
	public GameObject LocalBubblePrefab;
	
	const float FADE_DURATION = 0.25f;
	
	List<GameObject> TextBubbles = new List<GameObject>();
	
	
	private bool FadingIn = false;
	
    // Start is called before the first frame update
    void Start()
    {
		/*Dialogue test = new Dialogue("a", Speaker.remote);
		test.AddDialogue("b", Speaker.remote).AddDialogue("c", Speaker.remote);
        StartCoroutine(PrintConversation(test));*/
    }

    public void PrintDialogue(Dialogue dialogue)
	{
		if (dialogue == null)
		{
			Debug.Log("No dialogue");
			return;
		}
		StartCoroutine(PrintConversation(dialogue));
	}
	
	public void ClearDialogue()
	{
		foreach(GameObject obj in TextBubbles)
		{
			StartCoroutine(RemoveText(obj));
		}
		TextBubbles = new List<GameObject>();
		
		if (FadingIn) {
			StopCoroutine("PrintConversation");
			FadingIn = false;
		}
	}
	
	private IEnumerator RemoveText(GameObject bubble)
	{
		
		Image image = bubble.GetComponent<Image>();
		TMP_Text text = bubble.GetComponentInChildren<TMP_Text>();
		
		Color imageColor = image.color;
		Color textColor = text.color;
		
		float alpha = imageColor.a;
		float initialAlpha = imageColor.a;
		for (float elapsedTime = 0f; elapsedTime < FADE_DURATION; elapsedTime += Time.deltaTime)
		{
			alpha = Mathf.Lerp(initialAlpha, 0f, elapsedTime / FADE_DURATION);
			imageColor.a = alpha;
			textColor.a = alpha;
			
			image.color = imageColor;
			text.color = textColor;
			
			yield return null;
		}
		
		Destroy(bubble);
	}
	
	private IEnumerator PrintConversation(Dialogue dialogue)
	{	
		FadingIn = true;
		while (dialogue != null)
		{
			if (dialogue.Text == "")
			{
				dialogue = dialogue.Next;
				continue;
			}
			
			GameObject uiInstance;
			
			if (dialogue.Talker == Speaker.local)
			{
				uiInstance = Instantiate(LocalBubblePrefab, transform);
			}
			else
			{
				uiInstance = Instantiate(RemoteBubblePrefab, transform);
			}

			Image image = uiInstance.GetComponent<Image>();
            // Set the text of the TMP component
            TMP_Text textMesh = uiInstance.GetComponentInChildren<TMP_Text>();
            if (textMesh != null)
            {
                textMesh.text = dialogue.Text;
            }

			uiInstance.SetActive(true);
			
			
			Color imageColor = image.color;
			Color textColor = textMesh.color;
			
			Vector2 targetPos;
			if (TextBubbles.Count > 0)
			{
				targetPos.y = TextBubbles.Last().GetComponent<RectTransform>().anchoredPosition.y - 300f;
			}
			else
			{
				targetPos.y = 1700f;
			}
			
			RectTransform imageTransform = image.GetComponent<RectTransform>();
			targetPos.x = imageTransform.anchoredPosition.x;
			Vector2 initialPos = imageTransform.anchoredPosition;
			
			float elapsedTime = 0f;
			while (elapsedTime < FADE_DURATION)
			{
				imageColor.a = Mathf.Lerp(0f, 1f, elapsedTime / FADE_DURATION);
				textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / FADE_DURATION);
				
				image.color = imageColor;
				textMesh.color = textColor;
				
				imageTransform.anchoredPosition = Vector2.Lerp(initialPos, targetPos, elapsedTime / FADE_DURATION);
				
				elapsedTime += Time.deltaTime;
				
				yield return null;
			}
			
			imageColor.a = 1f;
			textColor.a = 1f;
			image.color = imageColor;
			textMesh.color = textColor;
			imageTransform.anchoredPosition = targetPos;
			TextBubbles.Add(uiInstance);
			
            // Gradually fade in the image and text
            /*CanvasGroup canvasGroup = uiInstance.GetComponent<CanvasGroup>();
			if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
            }
			
            if (canvasGroup != null)
            {
                float elapsedTime = 0f;
                while (elapsedTime < FADE_DURATION)
                {
                    canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / FADE_DURATION);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                canvasGroup.alpha = 1f; // Ensure full opacity
            }*/

            // Wait for a moment before proceeding to the next string
            yield return new WaitForSeconds(1.2f);
			
			dialogue = dialogue.Next;
		}		
		
		FadingIn = false;
		
	}
}
