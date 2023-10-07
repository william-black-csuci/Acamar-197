using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonGroup : MonoBehaviour
{
	private List<Image> Images = new List<Image>();
	private List<string> Labels = new List<string>();
	[SerializeField]
	private Sprite UnselectedSprite;
	[SerializeField]
	private Sprite SelectedSprite;
	private int Selected = -1;
	
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
		{
			Images.Add(child.GetComponent<Image>());
			Labels.Add(child.Find("Label").GetComponent<TMP_Text>().text);
		}
    }
	
	public void ButtonClicked(string label)
	{
		for (int i = 0; i < Labels.Count; i++)
		{
			if (label == Labels[i])
			{
				if (Selected != -1)
				{
					Images[Selected].sprite = UnselectedSprite;
				}
				Images[i].sprite = SelectedSprite;
				Selected = i;
				return;
			}
		}
		Debug.LogError("Mismatched label on button pressed!");
	}
	
	public void Unselect()
	{
		if (Selected != -1)
		{
			Images[Selected].sprite = UnselectedSprite;
			Selected = -1;
		}
	}
	
	public string GetSelected()
	{
		if (Selected == -1)
		{
			return "";
		}
		
		return Labels[Selected];
	}
}
