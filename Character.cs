using Godot;
using System.Collections.Generic;

public class CharacterInfo
{
	public string name { get; }
	public string gender { get; }
	public SpriteFrames charImg { get; }

	public CharacterInfo(string name, string gender, SpriteFrames charImg = null)
	{
		this.name = name;
		this.gender = gender;
		this.charImg = charImg;
	}
}

public class CharacterDatabase
{
	public readonly Dictionary<string, CharacterInfo> CHARACTER_INFORMATION = new()
	{
		{"Beverly",
		new CharacterInfo("Beverly", "female", GD.Load<SpriteFrames>("res://characters/Beverly.tres"))},
		{"Snake",
		new CharacterInfo("Snake", "male", GD.Load<SpriteFrames>("res://characters/Snake.tres"))},
		{"Owl",
		new CharacterInfo("Owl", "male", GD.Load<SpriteFrames>("res://characters/Owl.tres"))},
		{"Monkey",
		new CharacterInfo("Monkey", "male", GD.Load<SpriteFrames>("res://characters/Monkey.tres"))}
	};

	public SpriteFrames getCharInfo(string name)
	{
		if (CHARACTER_INFORMATION.TryGetValue(name, out var characterInfo))
		{
			return characterInfo.charImg;
		}
		else
		{
			return characterInfo.charImg;
		}
	}
}
