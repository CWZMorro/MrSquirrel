using Godot;
using System;

public partial class CharacterSprite : Node2D
{

	private AnimatedSprite2D animatedSprite;
	private CharacterDatabase charData = new CharacterDatabase();
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
		animatedSprite.SpriteFrames = charData.getCharInfo(name);

		if (expression.Equals("talking"))
		{
			animatedSprite.Play("talking");
		}
		else
		{
			animatedSprite.Play("idle");
		}
	}

	public void PlayIdleAnimation()
	{
		animatedSprite.Play("idle");
	}
}
