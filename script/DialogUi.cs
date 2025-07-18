using Godot;
using System;

public partial class DialogUi : Control
{
	[Signal]
	public delegate void AnimationDoneEventHandler();
	private RichTextLabel dialogLine;
	private Label speakerName;
	// Called when the node enters the scene tree for the first time.
	private int ANIMATION_SPEED = 30;
	private bool animateText = false;
	private int currentVisibleChar = 0;
	
	public override void _Ready()
	{
		dialogLine = GetNode<RichTextLabel>("DialogBox/DialogLine");
		speakerName = GetNode<Label>("SpeakerBox/SpeakerName");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (animateText)
		{
			if (dialogLine.VisibleRatio < 1)
			{
				dialogLine.VisibleRatio += (float)((1.0 / dialogLine.Text.Length) * (ANIMATION_SPEED * delta));
				currentVisibleChar = dialogLine.VisibleCharacters;
			}
			else
			{
				animateText = false;
				EmitSignal(SignalName.AnimationDone);
			}
		}
	}

	public void setDialogLine(string text)
	{
		dialogLine.Text = text;
	}

	public void setSpeakerName(string name)
	{
		speakerName.Text = name;
	}

	public void setAnimateText(bool animation)
	{
		animateText = animation;
	}

	public bool getAnimateText()
	{
		return animateText;
	}
	public void changeLine(string name, string line)
	{
		speakerName.Text = name;
		currentVisibleChar = 0;
		dialogLine.Text = line;
		animateText = true;
		dialogLine.VisibleCharacters = 0;
	}

	public void SkipTextAnimation()
	{
		dialogLine.VisibleRatio = 1;
	}
}
