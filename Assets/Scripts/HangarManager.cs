using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HangarManager : MonoBehaviour
{
	public GameObject StaticSound;
	
	private Dictionary<string, Encounter> Zones = new Dictionary<string, Encounter>();
	/*{
		{"P1", null},
		{"A1", null},
		{"A2", null},
		{"A3", null},
		{"A4", null},
		{"A5", null},
		{"A6", null},
		{"Inbound", null}
	};*/
	
	[SerializeField]
	private GameObject HangarStatic;
	[SerializeField]
	private GameObject InboundStatic;
	[SerializeField]
	private string[] TokenZones;
	[SerializeField]
	private Image[] TokenImages;
	[SerializeField]
	private Sprite Transparent;
	
	private Dictionary<string, Image> Tokens = new Dictionary<string, Image>();
	
	[SerializeField]
	private Image InboundImage;
	
	private const float STATIC_TIME = 0.3f;
	
    // Start is called before the first frame update
    void Start()
    {
		//InboundImage = GameObject.FindWithTag("Inbound Ship").GetComponent<Image>();
		
		for (int i = 0; i < TokenZones.Length && i < TokenImages.Length; i++)
		{
			Tokens.Add(TokenZones[i], TokenImages[i]);
			Zones.Add(TokenZones[i], null);
		}
		
		foreach (Transform child in Hidden.transform)
		{
			Hangars.Add(child.gameObject);
		}
		SetActiveHangar("A");
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
	
	public Encounter ClearHangar(string zone)
	{
		if (zone == "Inbound")
		{
			if (Zones["Inbound"] == null)
			{
				return null;
			}
			
			StartCoroutine(ClearInbound());
			Encounter ship = Zones["Inbound"];
			Zones["Inbound"] = null;
			Tokens[zone].sprite = Transparent;
			return ship;
		}
		else if (Zones.ContainsKey(zone))
		{
			if (Zones[zone] == null)
			{
				return null;
			}
			
			StartCoroutine(ClearHangar());
			Color spriteColor = Tokens[zone].color;
			spriteColor.a = 0f;
			Tokens[zone].color = spriteColor;
			Encounter ship = Zones["Inbound"];
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
		StaticSound.SetActive(true);
		
		for (float elapsedTime = 0f; elapsedTime < STATIC_TIME; elapsedTime += Time.deltaTime)
		{
			yield return null;
		}
		
		HangarStatic.SetActive(false);
		StaticSound.SetActive(false);
	}
	
	public Encounter GetShip(string zone)
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
	
	public void AddToken(Encounter ship, string zone)
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
		
		//Debug.Log(zone);
		Zones[zone] = ship;
		Tokens[zone].sprite = ship.Image;
		//ship.SetToken(Tokens[zone]);
		Color color = Tokens[zone].color;
		color.a = 1f;
		Tokens[zone].color = color;
	}
	
	private IEnumerator RunInboundStatic()
	{
		InboundStatic.SetActive(true);
		StaticSound.SetActive(true);
		
		for (float elapsedTime = 0f; elapsedTime < STATIC_TIME; elapsedTime += Time.deltaTime)
		{
			yield return null;
		}
		
		InboundStatic.SetActive(false);
		StaticSound.SetActive(false);
	}
	
	private IEnumerator RunHangarStatic()
	{
		HangarStatic.SetActive(true);
		StaticSound.SetActive(true);
		
		for (float elapsedTime = 0f; elapsedTime < STATIC_TIME; elapsedTime += Time.deltaTime)
		{
			yield return null;
		}
		
		HangarStatic.SetActive(false);
		StaticSound.SetActive(false);
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
	
	private GameObject CurrentHangar;
	[SerializeField]
	private GameObject Hidden;
	[SerializeField]
	private GameObject Active;
	private List<GameObject> Hangars = new List<GameObject>();
	
	public void SetActiveHangar(string name)
	{
		if (CurrentHangar == null || name != CurrentHangar.name)
		{
			if (CurrentHangar != null)
			{
				CurrentHangar.transform.SetParent(Hidden.transform);
				StartCoroutine(RunHangarStatic());
			}
			
			foreach (GameObject hangar in Hangars)
			{
				if (hangar.name == name)
				{
					CurrentHangar = hangar;
					hangar.transform.SetParent(Active.transform);
					return;
				}
			}
			
			Debug.LogError(String.Format("Failed to find hangar: {0}", name));
		}
	}
	
	public float GetOccupied()
	{
		int max = 0;
		int occupied = 0;
		
		foreach (Encounter ship in Zones.Values)
		{
			max++;
			if (ship != null)
			{
				occupied++;
			}
		}
		
		return (float)occupied / (float)max;
	}
	
}
