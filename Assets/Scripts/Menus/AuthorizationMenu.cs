using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorizationMenu : Menu
{
	private ButtonGroup Hangar;
	private ButtonGroup Number;
	
	protected override void Start()
	{
		base.Start();
		
		Hangar = transform.Find("Mask").Find("Hangar Selection").GetComponent<ButtonGroup>();
		Number = transform.Find("Mask").Find("Number Selection").GetComponent<ButtonGroup>();
	}
	
	public string GetSelectedHangar()
	{
		return Hangar.GetSelected() + Number.GetSelected();
	}
}
