using Godot;
using System;

public partial class MainScene : Node2D
{
	private Node character;
	private Node dialogUi;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		character = GetNode("%Character");
		dialogUi = GetNode("%DialogUi");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _Process(double delta)
	{
	}

	public static readonly string[] dialogLines = {
		"Beverly: Hi, Mr Snake.",
		"Snake: The outcome is Mr Owl crashing out and then after Ms Beverly begs for him to tell the truth he apologizes for accusing " +
		"Mr Snake. The players tells him that smart people make hard decisions and have to face truths that may hurt themselves. ",
		"Beverly: Ya'll just wanted to know what really happens to Mr Squirrel. We're sorry for taking him away from you.",
		"Mr Snake: Mr Monkey, we may not have been good friends, but I assure you Mr Squirrel wouold be pleased knowing you spoke the truth."
	};
}
