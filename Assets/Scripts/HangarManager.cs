using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangarManager : MonoBehaviour
{
	private Dictionary<string, Ship> Zones = new Dictionary<string, Ship>()
	{
		{"P1", null},
		{"A1", null},
		{"A2", null},
		{"A3", null},
		{"A4", null},
		{"A5", null},
		{"A6", null},
		{"Inbound", null}
	};
	
	[SerializeField]
	private GameObject HangarStatic;
	[SerializeField]
	private GameObject InboundStatic;
	[SerializeField]
	private string[] TokenZones;
	[SerializeField]
	private Image[] TokenImages;
	
	private Dictionary<string, Image> Tokens = new Dictionary<string, Image>();
	
	private Image InboundImage;
	
	private const float STATIC_TIME = 0.3f;
	
    // Start is called before the first frame update
    void Start()
    {
		InboundImage = GameObject.FindWithTag("Inbound Ship").GetComponent<Image>();
		
		for (int i = 0; i < TokenZones.Length && i < TokenImages.Length; i++)
		{
			Tokens.Add(TokenZones[i], TokenImages[i]);
		}
		
    }

    public bool IsAvailable(string zone)
	{
		if (Zones.ContainsKey(zone) && Zones[zone] == null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	private IEnumerator ClearInbound()
	{
		
		InboundStatic.SetActive(true);
		
		
		Color spriteColor = InboundImage.color;
		spriteColor.a = 0f;
		InboundImage.color = spriteColor;
		
		for (float elapsedTime = 0f; elapsedTime < STATIC_TIME; elapsedTime += Time.deltaTime)
		{
			yield return null;
		}
		
		InboundStatic.SetActive(false);
	}
	
	public Ship ClearHangar(string zone)
	{
		if (zone == "Inbound")
		{
			if (Zones["Inbound"] == null)
			{
				return null;
			}
			
			StartCoroutine(ClearInbound());
			Ship ship = Zones["Inbound"];
			Zones["Inbound"] = null;
			return ship;
		}
		else if (Zones.ContainsKey(zone))
		{
			if (Zones[zone] == null)
			{
				return null;
			}
			
			StartCoroutine(ClearHangar());
			Color spriteColor = Zones[zone].Token.color;
			spriteColor.a = 0f;
			Zones[zone].Token.color = spriteColor;
			Ship ship = Zones["inbound"];
			Zones[zone] = null;
			
			return ship;
		}
		else
		{
			Debug.LogError("Invalid Hangar Zone!");
			return null;
		}
	}
	
	private IEnumerator ClearHangar()
	{
		HangarStatic.SetActive(true);
		
		for (float elapsedTime = 0f; elapsedTime < STATIC_TIME; elapsedTime += Time.deltaTime)
		{
			yield return null;
		}
		
		HangarStatic.SetActive(false);
	}
	
	public Ship GetShip(string zone)
	{
		if (Zones.ContainsKey(zone) == false)
		{
			return null;
		}
		else
		{
			return Zones[zone];
		}
	}
	
	public void AddToken(Ship ship, string zone)
	{
		if (Zones.ContainsKey(zone) == false || Zones[zone] != null)
		{
			return;
		}
		
		if (zone == "Inbound")
		{
			StartCoroutine(RunInboundStatic());
		}
		else
		{
			StartCoroutine(RunHangarStatic());
		}
		
		Zones[zone] = ship;
		ship.SetToken(Tokens[zone]);
		Color color = Tokens[zone].color;
		color.a = 1f;
		Tokens[zone].color = color;
	}
	
	private IEnumerator RunInboundStatic()
	{
		InboundStatic.SetActive(true);
		
		for (float elapsedTime = 0f; elapsedTime < STATIC_TIME; elapsedTime += Time.deltaTime)
		{
			yield return null;
		}
		
		InboundStatic.SetActive(false);
	}
	
	private IEnumerator RunHangarStatic()
	{
		HangarStatic.SetActive(true);
		
		for (float elapsedTime = 0f; elapsedTime < STATIC_TIME; elapsedTime += Time.deltaTime)
		{
			yield return null;
		}
		
		HangarStatic.SetActive(false);
	}
	
	public string GetAvailableZone()
	{
		string zone = TokenZones[(int)Mathf.Floor(UnityEngine.Random.Range(0, TokenZones.Length))];
		
		while(Zones[zone] != null)
		{
			zone = TokenZones[(int)Mathf.Floor(UnityEngine.Random.Range(0, TokenZones.Length))];
		}
		
		return zone;
	}
}
