using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomShipData
{
    private static List<string> Names = new List<string>
	{
		"Starshard",
		"Celestia",
		"Nebulon",
		"Galactrix",
		"Astraeus",
		"Lunaris",
		"Orionis",
		"Pulsaris",
		"Quantum",
		"Astroblade",
		"Stellarion",
		"Solarflare",
		"Andromis",
		"Cosmicraft",
		"Stardrive",
		"Lunafire",
		"Novastra",
		"Starhawk",
		"Vortexia",
		"Nebulos",
		"Solarian",
		"Zenithar",
		"Astraflux",
		"Lunavoy",
		"Pulsaris",
		"Interstelis",
		"Celestar",
		"Stardust",
		"Quantumix",
		"Orionix",
		"Starclad",
		"Nebulite",
		"Solarnaut",
		"Astranic",
		"Stellaria",
		"Novaride",
		"Galaxian",
		"Celestria",
		"Nebulonix",
		"Astranova",
		"Orionis",
		"Solarian",
		"Cosmius",
		"Stardustar",
		"Novaflare",
		"Pulsarix",
		"Stellarix",
		"Lunaris",
		"Nebularis",
		"Zenithar"
	};
	
	public static string GetRandomName()
	{
		if (Names.Count == 0)
		{
			return "";
		}
		
		string name = Names[(int)Random.Range(0, Names.Count)];
		Names.Remove(name);
		return name;
	}
}
