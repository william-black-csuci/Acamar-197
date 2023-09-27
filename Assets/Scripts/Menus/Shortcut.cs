using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour
{
	public Menu TargetMenu;
	
    public void OnClick()
	{
		TargetMenu.Activate();
	}
}
