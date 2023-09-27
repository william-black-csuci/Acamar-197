using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HangarCategory
{
	A,
	P
};

public class Event
{
	private Sprite[] FlyingSprites;
	private Sprite[] LandedSprites;
	private float FrameRate = 10f;
	private int Frame = 0;
	private float FrameTime = 0f;
	private EventData Data;
	
	private const string FLYING_FOLDER = "Flying";
	private const string LANDED_FOLDER = "Landed";
	
	public Event(string spritePath, EventData data, float frameRate=10f)
	{
		FlyingSprites = Resources.LoadAll<Sprite>(spritePath + "/" + FLYING_FOLDER);
		LandedSprites = Resources.LoadAll<Sprite>(spritePath + "/" + LANDED_FOLDER);
		Data = data;
		FrameRate = frameRate;
	}
	
	public void AnimateFlying(SpriteRenderer sprite, float time)
	{
		UpdateFrame(time);	
		if (Frame >= FlyingSprites.Length)
		{
			Frame = 0;
		}
		sprite.sprite = FlyingSprites[Frame];
	}
	
	public void AnimateLanded(SpriteRenderer sprite, float time)
	{
		UpdateFrame(time);
		if (Frame >= LandedSprites.Length)
		{
			Frame = 0;
		}
		sprite.sprite = LandedSprites[Frame];
	}
	
	private void UpdateFrame(float time)
	{
		FrameTime += time;
		if (FrameTime >= 1f / FrameRate)
		{
			FrameTime -= 1f / FrameRate;
			Frame++;
		}
	}
}
