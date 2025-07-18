using Godot;
using System;
using System.Collections.Generic;

public enum Character
	{
		Beverly,
		Snake,
		Owl,
		Squirrel,
		Monkey,
		Eagle,
		Canary,
		Parrot,
		Frog,
		Swan,
		Hedgehog,
		Player
	}
public class CharacterInfo
{
	public Character name { get; }
	public string gender { get; }
	public SpriteFrames charImg { get; }

	public CharacterInfo(Character name, string gender, SpriteFrames charImg = null)
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
		new CharacterInfo(Character.Beverly, "female", GD.Load<SpriteFrames>("res://characters/Beverly.tres"))},
		{"Snake",
		new CharacterInfo(Character.Snake, "male", GD.Load<SpriteFrames>("res://characters/Snake.tres"))},
		{"Owl",
		new CharacterInfo(Character.Owl, "male")},

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
