using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InboundShip : Ship
{
	public override void OnAuthorize()
	{
		base.OnAuthorize();
		GameObject.FindWithTag("Hangar").GetComponent<HangarManager>().AddToken(this, Zone);
	}
}
