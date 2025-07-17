using Godot;
using System;
using System.Collections.Generic;

public partial class MainScene : Node2D
{
	private Character character;
	private DialogUi dialogUi;
	private int dialogIndex = 0;
	public static readonly string[] dialogLines = {
		"Beverly: Hi, Mr Snake.",
		"Snake: The outcome is Mr Owl crashing out and then after Ms Beverly begs for him to tell the truth he apologizes for accusing " +
		"Mr Snake. The players tells him that smart people make hard decisions and have to face truths that may hurt themselves. ",
		"Beverly: Ya'll just wanted to know what really happens to Mr Squirrel. We're sorry for taking him away from you.",
		"Snake: Mr Monkey, we may not have been good friends, but I assure you Mr Squirrel wouold be pleased knowing you spoke the truth."
	};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		dialogIndex = 0;
		character = GetNode<Character>("CanvasLayer2/Character/Character");
		dialogUi = GetNode<DialogUi>("CanvasLayer2/DialogUI");

		ProcessCurrentLine();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("nextLine"))
		{
			if (dialogIndex < dialogLines.Length - 1)
			{
				dialogIndex += 1;
				ProcessCurrentLine();
			}
		}
	}


	//Convert dialog lines, separating Speaker and dialog in a dictionary.
	public Dictionary<string, string> ParseLine(string line)
	{
		String[] lineInfo = line.Split(":");

		//Error handling
		if (lineInfo.Length < 2)
		{
			GD.PushError("Line does not contain ':'");
			return new Dictionary<string, string>();
		}

		return new Dictionary<string, string>
		{
			{"speakerName", lineInfo[0].Trim()},
			{"dialogLine", lineInfo[1].Trim()}
		};
	}

	public void ProcessCurrentLine()
	{
		var line = dialogLines[dialogIndex];
		var lineInfo = ParseLine(line);
		dialogUi.changeLine(lineInfo["speakerName"], lineInfo["dialogLine"]);
		dialogUi.setDialogLine(lineInfo["dialogLine"]);
		character.ChangeCharacter(lineInfo["speakerName"]);
	}
}
