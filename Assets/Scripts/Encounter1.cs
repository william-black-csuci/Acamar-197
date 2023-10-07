using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Encounter1 : Encounter
{
	private List<string> Greetings = new List<string>
	{
		"This is the $, requesting clearance for a smooth landing.",
		"Greetings, Acamar 197. I am the pilot of the $, requesting permission to land.",
		"Pilot of the $ speaking. We're here for refueling and supplies, over.",
		"Vessel $ reporting in, ready for docking instructions.",
		"Hello, Acamar 197 control. We are the $, requesting permission to land.",
		"This is the $. Transmitting identification and requesting landing authorization.",
		"This is the $. Requesting permission to enter Acamar 197's airspace.",
		"Pilot of the $, seeking permission to dock for routine maintenance.",
		"Greetings, Acamar 197 control. This is the $. May we land?",
		"This is the $. Requesting clearance to enter your spaceport.",
		"Identifying as the $. We're here for a scheduled cargo transfer.",
		"We are the $, requesting access to Acamar 197's port.",
		"Hello, Acamar 197 control. We are the $, requesting permission to dock.",
		"This is the $. Requesting permission to land.",
		"Pilot of the $ here, seeking permission to dock for cargo unloading.",
		"Greetings, Acamar 197 control. This is the $. Can we proceed to land?",
		"Vessel $ reporting in, ready for instructions on landing.",
		"Hello, Acamar 197 control. We are the $, requesting permission to dock.",
		"This is the $, transmitting clearance and requesting authorization to land.",
		"Pilot of the $, seeking permission to dock for scheduled maintenance.",
		"Greetings, Acamar 197 control. This is the $. May we land?",
		"This the $. Requesting clearance to dock."
	};
	
	private List<string> DeniedText = new List<string>
	{
		"Acknowledged, but we're not happy about it. We expected better.",
		"Acknowledged, control. We'll find another place to land.",
		"Acknowledged. We'll find an alternative landing solution. Good day.",
		"Acknowledged. We'll keep our distance for now, control.",
		"Alright, we'll divert our course. But this isn't over.",
		"Alright, we'll divert our course. But this isn't the end of it.",
		"We'll follow your decision, reluctantly.",
		"Denied? Are you sure? We have urgent cargo to deliver!",
		"Denied? Not what we expected from this spaceport. We'll comply.",
		"Denied? Unbelievable! We'll remember this, Acamar 197.",
		"Denied? We're not happy about it, but we'll comply. For now.",
		"Denied? Well, you're not making any friends today.",
		"Denied? You're making a grave error, control.",
		"Denied? You're pushing your luck, traffic control.",
		"Fine, we'll comply. But mark our words, this won't be forgotten.",
		"Fine, we'll go elsewhere. Hope your spaceport isn't this strict with everyone.",
		"Okay, we'll divert our course. We expected better from Acamar 197.",
		"This is unacceptable! We'll remember this when we file our report.",
		"This is outrageous! You'll hear from our lawyers about this.",
		"We'll follow your rules, but this won't be forgotten, Acamar 197.",
		"We'll respect your decision, control, even though it's disappointing.",
		"Well, you leave us no choice. We'll go elsewhere.",
		"You'll regret this decision, control. Mark my words.",
		"You're being unreasonable, control. We have no choice.",
		"You're being unreasonable, control. We have no other options.",
		"You're making a mistake, control. We have important cargo to unload."
	};
	
	private List<string> AuthorizedText = new List<string>
	{
		"Acknowledged, control. Initiating landing sequence now.",
		"Acknowledged, control. We're initiating landing now.",
		"Acknowledged. Preparing for a smooth landing, control.",
		"Affirmative, control. We'll follow your instructions to land.",
		"Affirmative. Lowering landing gear and initiating descent.",
		"Affirmative. Lowering landing gear and preparing for descent.",
		"Authorization confirmed. Preparing for touchdown at your spaceport.",
		"Clearance accepted. Preparing for touchdown at Acamar 197.",
		"Clearance accepted. We're descending to the designated zone.",
		"Excellent! We're coming in for a gentle landing. See you on the surface.",
		"Good news! We're preparing for a smooth descent and touchdown.",
		"Great news! Initiating descent and approach to the landing pad.",
		"Great news! We'll commence our descent and approach.",
		"Great news, control! Initiating landing sequence.",
		"Landing authorization accepted. Descending to the landing zone.",
		"Landing clearance confirmed. We're on our way down.",
		"Landing clearance received. Beginning our descent now.",
		"Landing clearance received. Beginning our descent.",
		"Received and acknowledged. We're on our way to the landing zone.",
		"Received and confirmed. We're on our way to the landing pad.",
		"Received and understood. Commencing landing procedures.",
		"Roger that, control. Beginning final approach.",
		"Roger that, control. Commencing final approach and landing.",
		"Roger that, control. Final approach and landing engaged.",
		"Roger that, control. Final approach and landing initiated.",
		"Thank you for the clearance. Beginning descent for landing.",
		"Thank you for the clearance. Preparing for a safe landing.",
		"Thank you for the clearance. We're coming in for a safe landing.",
		"Thank you, control. Beginning our descent for landing.",
		"Understood, control. Initiating landing procedures.",
		"Understood, control. We're commencing landing procedures.",
		"Understood. Lowering landing gear and approaching the designated pad.",
		"We appreciate the authorization, control. Landing procedures initiated.",
		"We're all set to land. Much appreciated, control.",
		"We're all set to land. Thanks for the authorization."
	};
	
	public override IEnumerator StartEncounter()
	{	
		Image = Resources.Load<Sprite>("Images/Ships/Ship") as Sprite;
		
		HangarManager hangar = GameObject.FindWithTag("Hangar").GetComponent<HangarManager>();
		Authorized = UnityEngine.Random.value > hangar.GetOccupied() ? true : false;
		
		StartDialogue = new Dialogue("You have entered Acamar 197 space. Please identify yourself and transfer clearance codes to land.", Speaker.local);
		StartDialogue.AddDialogue(Greetings[UnityEngine.Random.Range(0, Greetings.Count)].Replace("$", RandomShipData.GetRandomName()), Speaker.remote);
		
		ClearanceGenerator clearance = GameObject.FindWithTag("Clearance Generator").GetComponent<ClearanceGenerator>();
		string codes = Authorized ? clearance.GetRealCode() : clearance.GetFakeCode();
		Dialogue codeDialogue = StartDialogue.AddDialogue(codes, Speaker.remote);
		
		yield return StartCoroutine(base.StartEncounter());
	}
	
	public override IEnumerator Deny()
	{
		Dialogue denyDialogue = new Dialogue("Your request to land has been denied.", Speaker.local);
		denyDialogue.AddDialogue(DeniedText[UnityEngine.Random.Range(0, DeniedText.Count)], Speaker.remote);
		denyDialogue.AddDialogue("Have a nice day, control out.", Speaker.local);
		
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.PrintDialogue(denyDialogue);
		
		yield return new WaitForSeconds(6f);
		
		StartCoroutine(base.Deny());
	}
	
	public override IEnumerator Authorize()
	{
		Dialogue authorizeDialogue = new Dialogue("You are cleared to land.", Speaker.local);
		authorizeDialogue.AddDialogue(AuthorizedText[UnityEngine.Random.Range(0, AuthorizedText.Count)], Speaker.remote);
		authorizeDialogue.AddDialogue("Welcome to Acamar 197.", Speaker.local);
		
		DialoguePrinter printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		printer.PrintDialogue(authorizeDialogue);
		
		yield return new WaitForSeconds(6f);
		
		StartCoroutine(base.Authorize());
	}
}
