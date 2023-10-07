using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

public class Level1Builder : MonoBehaviour
{
	private List<Ship> Ships = new List<Ship>();
	private HangarManager Hangar;
	//private TMP_InputField ZoneField;
	[SerializeField]
	private AuthorizationMenu Authorization;
	private DialoguePrinter Printer;
	[SerializeField]
	private TMP_FontAsset ClearanceFont;
	
	private string[] Zones = new string[]
	{
		"P1",
		"A1",
		"A2",
		"A3",
		"A4",
		"A5",
		"A6",
		"B1",
		"B2",
		"B3",
		"B4",
		"B5",
		"B6",
		"P2"
	};
	
	void Start()
	{
		Hangar = GameObject.FindWithTag("Hangar").GetComponent<HangarManager>();
		//ZoneField = GameObject.FindWithTag("Zone Field").GetComponent<TMP_InputField>();
		Printer = GameObject.FindWithTag("Printer").GetComponent<DialoguePrinter>();
		//Authorization = GameObject.FindWithTag("Authorization Menu").GetComponent<AuthorizationMenu>();
		/*int landed = 0;
		Ship ship;
		for (int i = 0; i < 12; i++)
		{
			if (landed == 0)
			{
				ship = new RandomInboundLevel1();
			}
			else if (landed >= 7)
			{
				ship = new RandomOutboundLevel1();
			}
			else
			{
				if (Random.value > 0.4f)
				{
					ship = new RandomInboundLevel1();
				}
				else
				{
					ship = new RandomOutboundLevel1();
				}
			}
		}*/
		
		/*Ship ship;
		for (int i = 0; i < 7; i++)
		{
			ship = new RandomInboundLevel1(ClearanceFont);
			ship.Font = ClearanceFont;
			Ships.Add(ship);
		}*/
		UpdateFeedback();
		StartCoroutine(RunLevel());
	}
	
	private IEnumerator RunLevel()
	{
		int encounterNum = 1;
		Encounter ship;
		for (;; encounterNum++)
		{
			switch (encounterNum)
			{
				case 4:
					ship = gameObject.AddComponent(typeof(BadCodeShip1)) as Encounter;
					break;
				case 10:
					ship = gameObject.AddComponent(typeof(MedicalShip1)) as Encounter;
					break;
				default:
					ship = gameObject.AddComponent(typeof(Encounter1)) as Encounter;
					break;
			}
			
			ship.Authorization = Authorization;
			
			yield return StartCoroutine(ship.StartEncounter());
			
			while (!Authorized && !Denied)
			{
				yield return null;
			}
			
			if (Authorized)
			{
				if (ship.Authorized)
				{
					Right++;
				}
				else
				{
					Wrong++;
				}
				
				yield return StartCoroutine(ship.Authorize());
			}
			else if (Denied)
			{
				if (ship.Authorized)
				{
					Wrong++;
				}
				else
				{
					Right++;
				}
				
				yield return StartCoroutine(ship.Deny());
			}
			
			
			Authorized = false;
			Denied = false;
			UpdateFeedback();
		}
		/*foreach(Ship ship in Ships)
		{
			yield return new WaitForSeconds(4f);
			
			Hangar.AddToken(ship, "Inbound");
			
			yield return new WaitForSeconds(2f);
			
			ship.OnIntroduce();
			
			while (!Authorized && !Denied)
			{
				yield return null;
			}
			
			if (Authorized)
			{
				yield return new WaitForSeconds(0.1f);
				Hangar.ClearHangar("Inbound");
				//Debug.Log(ZoneSelect);
				Hangar.AddToken(ship, ZoneSelect);
				Authorized = false;
				Denied = false;
				
				if (ship.Authorized)
				{
					Right++;
				}
				else
				{
					Wrong++;
				}
			}
			else if (Denied)
			{
				yield return new WaitForSeconds(0.1f);
				Hangar.ClearHangar("Inbound");
				Authorized = false;
				Denied = false;
				
				if (ship.Authorized)
				{
					Wrong++;
				}
				else
				{
					Right++;
				}
			}
			
			Printer.ClearDialogue();
			UpdateFeedback();
			
		}*/
		
	}
	private int Right = 0;
	private int Wrong = 0;
	private string ZoneSelect;
	private bool Authorized = false;
	private bool Denied = false;
	
	public void OnAuthorize()
	{
		
		Debug.Log(String.Format("Zones: {0}", Zones));
		RightText.text = "hi";
		Debug.Log(String.Format("Authorization: {0}", Authorization));//.GetSelectedHangar());
		WrongText.text = "hi";
		Debug.Log(String.Format("Selected Hangar: {0}", Authorization.GetSelectedHangar()));
		Debug.Log(String.Format("Hangar: {0}", Hangar));
		RightText.text = "bye";
		if (Zones.Contains(Authorization.GetSelectedHangar()) && Hangar.IsAvailable(Authorization.GetSelectedHangar()))
		{
			Authorized = true;
			ZoneSelect = Authorization.GetSelectedHangar();
		}
		//Debug.Log("Authorize");
	}
	
	public void OnDeny()
	{
		Denied = true;
		//Debug.Log("Deny");
	}
	
	public TMP_Text RightText;
	public TMP_Text WrongText;
	
	private void UpdateFeedback()
	{
		RightText.text = String.Format("Right: {0}", Right);
		WrongText.text = String.Format("Wrong: {0}", Wrong);
	}
	
	// LEVEL 1
	// 1: inbound accept
	// 2-4: random
	// 5: stupid clearance code guy (deny)
	// 6-9: random
	// 10: medical emergency (deny)
	// 11: random
	// 12: slave trader (accept)
	
	// wait
	// send ship
	// repeat
	// wait
	// end level
	
	
	// SEND SHIP
	// put ship in zone
	// wait
	// send dialogue
	// wait for command
		// for inbound, wait for denied or zone
		// for outbound, wait for denied or accepted
	// wait
	// send dialogue (optional)
	// wait
	// execute command
		// remove old token
		// add new token
		
}
