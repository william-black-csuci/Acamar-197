using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ship
{
	public Image Token { get; private set; }
	private Sprite ShipSprite;
	public bool Authorized;
	private Dialogue Greeting;
	private Dialogue AuthorizeDialogue;
	private Dialogue DenyDialogue;
	public string Zone = "Inbound";
	public string Target = ""; 
	
	public void SetToken(Image token)
	{
		Token = token;
		//Token.sprite = ShipSprite;
	}
	
	protected void AddGreeting(string text, Speaker speaker)
	{
		Dialogue dialogue = Greeting;
		
		if (dialogue == null)
		{
			dialogue = new Dialogue(text, speaker);
		}
		else
		{
			dialogue.AddDialogue(text, speaker);
		}
		
		Greeting = dialogue;
	}
	
	protected void AddAuthorizedFarewell(string text, Speaker speaker)
	{
		Dialogue dialogue = AuthorizeDialogue;
		
		if (dialogue == null)
		{
			dialogue = new Dialogue(text, speaker);
		}
		else
		{
			dialogue.AddDialogue(text, speaker);
		}
	}
	
	protected void AddDeniedFarewell(string text, Speaker speaker)
	{
		Dialogue dialogue = DenyDialogue;
		
		if (dialogue == null)
		{
			dialogue = new Dialogue(text, speaker);
		}
		else
		{
			dialogue.AddDialogue(text, speaker);
		}
	}
	
	protected void SetSprite(string name)
	{
		ShipSprite = Resources.Load("Images/Ships/" + name) as Sprite;
	}
	
	public virtual void OnIntroduce()
	{
		GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>().PrintDialogue(Greeting);
	}
	
	public virtual void OnAuthorize()
	{
		GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>().PrintDialogue(AuthorizeDialogue);
		GameObject.FindWithTag("Hangar").GetComponent<HangarManager>().ClearHangar(Zone);
	}
	
	public virtual void OnDeny()
	{
		GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>().PrintDialogue(DenyDialogue);
		GameObject.FindWithTag("Hangar").GetComponent<HangarManager>().ClearHangar(Zone);
	}
}
