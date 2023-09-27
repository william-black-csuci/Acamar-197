using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomInboundLevel1 : InboundShip
{
	private string[] InboundGreetings = new string[]
	{
		"Hello Control",
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
	
    public RandomInboundLevel1()
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
		
		
		AddGreeting("Welcome to Acamar 197", Speaker.local);
		AddGreeting(ControlGreetings[UnityEngine.Random.Range(0, ControlGreetings.Length)], Speaker.local);
		AddGreeting(InboundGreetings[UnityEngine.Random.Range(0, InboundGreetings.Length)], Speaker.remote);
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
