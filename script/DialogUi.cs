using System;
using Godot;

public partial class DialogUi : Control
{
	[Signal]
	public delegate void AnimationDoneEventHandler();
	[Signal]
	public delegate void ChoiceSelectedEventHandler(string goTo);
	private RichTextLabel dialogLine;
	private Label speakerName;
	private CharacterSprite charSprite;
	private VBoxContainer choiceList;
	// Called when the node enters the scene tree for the first time.
	private int ANIMATION_SPEED = 30;
	private bool animateText = false;
	private int currentVisibleChar = 0;
	private PackedScene choiceButtonScene;

	public override void _Ready()
	{
		dialogLine = GetNode<RichTextLabel>("%DialogLine");
		speakerName = GetNode<Label>("%SpeakerName");
		choiceList = GetNode<VBoxContainer>("%ChoiceList");
		choiceButtonScene = GD.Load<PackedScene>("res://scene/playerChoice.tscn");
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

	public void DisplayChoices(Godot.Collections.Array choices)
	{
		foreach (Node child in choiceList.GetChildren())
		{
			child.QueueFree();
		}
		foreach (Godot.Collections.Dictionary choice in choices)
		{
			Button choiceButton = (Button)choiceButtonScene.Instantiate();
			choiceButton.Text = (string)choice["text"];
			choiceButton.Pressed += () => _on_choice_button_pressed((string)choice["goto"]);
			choiceList.AddChild(choiceButton);
		}
		choiceList.Show();
	}

	public void _on_choice_button_pressed(string goTo)
	{
		EmitSignal(SignalName.ChoiceSelected, goTo);
		choiceList.Hide();
	}
}
