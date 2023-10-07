using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCodeShip1 : Encounter
{
    public override IEnumerator StartEncounter()
	{
		Image = Resources.Load<Sprite>("Images/Ships/Ship") as Sprite;
		
		Authorized = false;
		
		StartDialogue = new Dialogue("You have entered Acamar 197 space. Please identify yourself and transfer clearance codes to land.", Speaker.local);
		StartDialogue.AddDialogue("Clearance codes? I mean... yeah I've got that right here...", Speaker.remote);
		StartDialogue.AddDialogue("uhhh... 1?", Speaker.remote);
		
		yield return StartCoroutine(base.StartEncounter());
	}
	
	public override IEnumerator Authorize()
	{
		Dialogue authorizeDialogue = new Dialogue("I have cleared you to land.", Speaker.local);
		authorizeDialogue.AddDialogue("That worked?! I mean, uh... acknowledged control.", Speaker.remote);
		authorizeDialogue.AddDialogue("Welcome to Acamar 197.", Speaker.local);
		
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.PrintDialogue(authorizeDialogue);
		
		yield return new WaitForSeconds(6f);
		
		yield return StartCoroutine(base.Authorize());
	}
	
	public override IEnumerator Deny()
	{
		Dialogue denyDialogue = new Dialogue("1 is not a valid clearance code.", Speaker.local);
		denyDialogue.AddDialogue("...2?", Speaker.remote);
		denyDialogue.AddDialogue("Your request to land has been denied. Have a nice day.", Speaker.local);
		
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.PrintDialogue(denyDialogue);
		
		yield return new WaitForSeconds(6f);
		
		yield return StartCoroutine(base.Deny());
	}
}
