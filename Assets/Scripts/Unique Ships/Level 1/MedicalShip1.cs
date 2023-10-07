using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalShip1 : Encounter
{
    public override IEnumerator StartEncounter()
	{
		Image = Resources.Load<Sprite>("Images/Ships/Ship") as Sprite;
		
		Authorized = false;
		
		StartDialogue = new Dialogue("You have entered Acamar 197 space. Please identify yourself and transfer clearance codes to land.", Speaker.local);
		StartDialogue.AddDialogue("This is Lietenant Mayers of the Moscow. The Ivory Empire has launched an attack on the Phoebe System!", Speaker.remote);
		StartDialogue.AddDialogue("I have 13 soldiers and 2 civilians in critical condition. We need to land now!", Speaker.remote);
		
		yield return StartCoroutine(base.StartEncounter());
	}
	
	public override IEnumerator Authorize()
	{
		Dialogue authorizeDialogue = new Dialogue("You have been granted emergency clearance to land.", Speaker.local);
		authorizeDialogue.AddDialogue("Thank you, we won't forget this.", Speaker.remote);
		
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.PrintDialogue(authorizeDialogue);
		
		yield return new WaitForSeconds(5.5f);
		
		yield return StartCoroutine(base.Authorize());
	}
	
	public override IEnumerator Deny()
	{
		Dialogue denyDialogue = new Dialogue("I'm sorry, but a clearance code is needed to land.", Speaker.local);
		denyDialogue.AddDialogue("Control are you serious? These people will die!", Speaker.remote);
		denyDialogue.AddDialogue("Have a nice day.", Speaker.local);
		
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.PrintDialogue(denyDialogue);
		
		yield return new WaitForSeconds(6f);
		
		yield return StartCoroutine(base.Deny());
	}
}
