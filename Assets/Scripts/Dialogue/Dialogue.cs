using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Speaker
{
	local,
	remote
};

public class Dialogue
{
	public string Text { get; private set; }
	
	public Dialogue Next { get; private set; } = null;
	
	public Speaker Talker { get; private set; }
	
    public Dialogue(string text, Speaker speaker)
	{
		Text = text;
		Talker = speaker;
	}
	
	public Dialogue AddDialogue(string text, Speaker speaker)
	{
		Dialogue last = this;
		while (last.Next != null)
		{
			last = last.Next;
		}
		
		last.Next = new Dialogue(text, speaker);
		return last.Next;
	}
	
	public void PrintDialogue(ref TextMeshProUGUI textBox)
	{
		textBox.text = Text;
	}
}
