using Godot;

public partial class MainScene : Node2D
{
	private CharacterSprite character;
	private DialogUi dialogUi;
	private int dialogIndex = 0;
	private Godot.Collections.Array dialogLines = [];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Program Initiated:");
		dialogLines = LoadDialog("res://resources/scene.json");
		dialogIndex = 0;
		character = GetNode<CharacterSprite>("CanvasLayer2/Character/CharacterSprite");
		dialogUi = GetNode<DialogUi>("CanvasLayer2/DialogUI");
		dialogUi.Connect("AnimationDone", new Callable(this, nameof(OnTextAnimationDone)));
		dialogUi.Connect("ChoiceSelected", new Callable(this, nameof(WhenChoiceSelected)));


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
			if (dialogUi.getAnimateText())
			{
				dialogUi.SkipTextAnimation();
			}
			else
			{
				if (dialogIndex < dialogLines.Count - 1)
				{
					dialogIndex += 1;
					ProcessCurrentLine();
				}
			}

		}
	}

	public void ProcessCurrentLine()
	{
		var line = dialogLines[dialogIndex].AsGodotDictionary();
		//int test += 1;
		GD.Print("current line: " + dialogIndex);
		//Check if there is "goto"
		GD.Print("Checking for 'goto'.....");
		if (line.ContainsKey("goto"))
		{
			//test = "goto";
			GD.Print("goto has been found");
			dialogIndex = GetAnchorPosition(line["goto"]);
			GD.Print(dialogIndex + " this is after 'goto' position has been found");
			ProcessCurrentLine();
			return;
		}

		//Check if there is "anchor"
		GD.Print("Checking for 'anchor'....");
		if (line.ContainsKey("anchor"))
		{
			GD.Print("anchor has been found");
			dialogIndex += 1;
			GD.Print("'anchor' positon has been found");
			ProcessCurrentLine();
			return;
		}
		GD.Print("nothing has been found.... reading line of dialog now");

		//Check if there is "choices"
		GD.Print("Checking for 'choices'.....");
		if (line.ContainsKey("choices"))
		{
			GD.Print("choices has been found");
			dialogUi.DisplayChoices(dialogLines);

		}
		else
		{
			//Reading line of dialog
			string speaker = line["speaker"].AsString();
			string text = line["text"].AsString();
			dialogUi.changeLine(speaker, text);
			dialogUi.setDialogLine(text);

			if (dialogUi.getAnimateText())
			{
				character.ChangeCharacter(speaker, "talking");
			}
			else
			{
				character.ChangeCharacter(speaker);
			}
		}
	}

	public void OnTextAnimationDone()
	{
		character.PlayIdleAnimation();
	}

	public void WhenChoiceSelected()
	{

	}

	public Godot.Collections.Array LoadDialog(string filepath)
	{
		using var file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
		if (file != null)
		{
			GD.Print("File exist");
			var content = file.GetAsText();
			if (content == null)
			{
				GD.Print("the content is empty!!");
				return null;
			}
			return Json.ParseString(content).AsGodotArray();
		}
		GD.Print("File does not exits!!");
		return null;
	}

	public int GetAnchorPosition(Variant anchor)
	{
		GD.Print(dialogIndex);
		GD.Print("Getting anchor position....");
		for (int i = 0; i < dialogLines.Count; i++)
		{
			var line = dialogLines[i].AsGodotDictionary();
			if (line.ContainsKey("anchor") && line["anchor"].AsString() == anchor.AsString())
			{
				GD.Print("anchor has been found");
				return i;
			}
		}
		GD.Print("ERROR: could not find anchor: " + anchor);
		GD.Print(dialogIndex);
		return -100;
	}
}
