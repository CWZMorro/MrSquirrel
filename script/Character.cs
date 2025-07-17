using Godot;
using System;
using System.Collections.Generic;

public partial class Character : Node2D
{

	private AnimatedSprite2D animatedSprite;
	private static readonly Dictionary<string, SpriteFrames> CHARACTER_FRAMES = new()
	{
		{"Beverly", GD.Load<SpriteFrames>("res://characters/Beverly.tres")},
		{"Snake", GD.Load<SpriteFrames>("res://characters/Snake.tres")}
	};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ChangeCharacter(string name, string expression = "idle")
	{
		animatedSprite.SpriteFrames = CHARACTER_FRAMES[name];

		if (expression == "talking")
		{
			animatedSprite.Play("talking");
		}
		else
		{
			animatedSprite.Play("idle");
		}
	}
}
