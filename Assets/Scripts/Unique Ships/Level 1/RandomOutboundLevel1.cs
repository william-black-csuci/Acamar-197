/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomOutboundLevel1 : InboundShip
{
	
	private string[] OutboundGreetings = new string[]
	{
		"Transferring codes now",
		"",
		"Here you go",
		"on it"
	};
	
	private string[] ControlGreetings = new string[]
	{
		"Transfer your clearance codes immediately",
		"Transfer clearance codes",
		"Transfer clearance codes immediately",
		"Transfer your clearance codes"
	};
	
    RandomOutboundLevel1()
	{
		SetSprite("Ship");
		
		if (UnityEngine.Random.value > 0.4f)
		{
			Authorized = true;
		}
		else
		{
			Authorized = false;
		}
		
		Zone = GameObject.FindWithTag("Hangar").GetComponent<HangarManager>().GetAvailableZone();
		
		AddGreeting(String.Format("Requesting authorization to takeoff from landing zone {0}", Zone), Speaker.remote);
		AddGreeting(ControlGreetings[UnityEngine.Random.Range(0, ControlGreetings.Length)], Speaker.local);
		AddGreeting(OutboundGreetings[UnityEngine.Random.Range(0, OutboundGreetings.Length)], Speaker.remote);
		if (Authorized)
		{
			AddGreeting(GameObject.FindWithTag("Clearance Generator").GetComponent<ClearanceGenerator>().GetRealCode().ToString(), Speaker.remote);
		}
		else
		{
			AddGreeting(GameObject.FindWithTag("Clearance Generator").GetComponent<ClearanceGenerator>().GetFakeCode().ToString(), Speaker.remote);
		}
	}
	
	
}
*/