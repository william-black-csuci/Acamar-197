using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Encounter : MonoBehaviour
{
	protected Dialogue StartDialogue;
	public Sprite Image { get; protected set; }
	public UnityEvent Finished { get; private set; } = new UnityEvent();
	public bool Authorized { get; protected set; }
	
	public AuthorizationMenu Authorization { protected get; set; }
	
	public virtual IEnumerator StartEncounter()
	{	
		yield return new WaitForSeconds(4f);
		
		HangarManager hangar = GameObject.FindWithTag("Hangar").GetComponent<HangarManager>();
		hangar.AddToken(this, "Inbound");
		
		yield return new WaitForSeconds(2f);
		
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.PrintDialogue(StartDialogue);
		
		Finished.Invoke();
	}
	
	public virtual IEnumerator Deny()
	{
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.ClearDialogue();
		
		yield return new WaitForSeconds(0.1f);
		
		HangarManager hangar = GameObject.FindWithTag("Hangar").GetComponent<HangarManager>();
		hangar.ClearHangar("Inbound");
		
		Finished.Invoke();
	}
	
	public virtual IEnumerator Authorize()
	{
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.ClearDialogue();
		
		yield return new WaitForSeconds(0.1f);
		
		HangarManager hangar = GameObject.FindWithTag("Hangar").GetComponent<HangarManager>();
		hangar.ClearHangar("Inbound");
		
		yield return new WaitForSeconds(0.1f);
		
		//AuthorizationMenu Authorization = GameObject.FindWithTag("Authorization Menu").GetComponent<AuthorizationMenu>();
		hangar.AddToken(this, Authorization.GetSelectedHangar());
		
		Finished.Invoke();
	}
}
