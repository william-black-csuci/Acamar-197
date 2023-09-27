using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class GifPlayer : MonoBehaviour
{
	private int Frame = 0;
	private float FPS = 10f;
	private float TimeElapsed = 0f;
	private Sprite[] Frames;
	private Image sprite;
	
	public string Path;
	
    // Start is called before the first frame update
    void Start()
    {
        Frames = Resources.LoadAll<Sprite>("Images/" + Path);
		Array.Sort(Frames, delegate(Sprite x, Sprite y) {return int.Parse(x.name).CompareTo(int.Parse(y.name)); });
		
		sprite = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeElapsed += Time.deltaTime;
		
		if (TimeElapsed >= 1f / FPS)
		{
			TimeElapsed -= 1f / FPS;
			
			Frame++;
			if (Frame >= Frames.Length)
			{
				Frame = 0;
			}
			
			sprite.sprite = Frames[Frame];
		}
    }
}
