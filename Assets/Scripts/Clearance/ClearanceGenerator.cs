using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClearanceGenerator : MonoBehaviour
{
	public List<string> Codes { get; private set; } = new List<string>();
	
	const int DEFAULT_CODE_AMOUNT = 25;
	const int CODE_SIZE = 16;
	
    // Start is called before the first frame update
    void Start()
    {
        GenerateRealCodes();
    }

   public void GenerateRealCodes(int amount = DEFAULT_CODE_AMOUNT)
   {
	   string code;
	   for (int i = 0; i < amount; i++)
	   {
		   code = GenerateCode();
		   
		   if (Codes.Contains(code))
		   {
			   i--;
			   continue;
		   }
		   else
		   {
			   Codes.Add(code);
		   }
	   }
   }
   
   public string GetFakeCode(int hints = 0, float similarity = 0f)
   {
	   string code = GenerateCode();
	   while (Codes.Contains(code))
	   {
		   code = GenerateCode();
	   }
	   
	   return code;
   }
   
   public string GetRealCode()
   {
	   return Codes[(int)Mathf.Floor(UnityEngine.Random.value * Codes.Count)];
   }
   
   private string GenerateCode()
   {
		string code = "";
		int ascii;
		for (int j = 0; j < CODE_SIZE; j++)
		{
			ascii = (int)(Mathf.Floor(UnityEngine.Random.value * 62f)) + 48;
	  
			if (ascii > 57)
			{
			   ascii += 7;
		   }
		   
		   if (ascii > 90)
		   {
			   ascii += 6;
		   }
		   
		   code += Convert.ToChar(ascii).ToString();
		}
		
		return code;
   }
}
