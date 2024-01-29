using System;
using Godot;
namespace YesNoSlap;

public partial class Game : Node {

  private static PackedScene DialogueScene = GD.Load<PackedScene>("res://scenes/hud/creator_balloon.tscn");

  [Export] private PlayerController _player;
  [Export] private EntertainmentBar _entertainmentBar;

  public string MenuScene = "res://scenes/MainMenu.tscn";

  private Resource? _creatorDialogue = GD.Load("res://narrative/Creator.dialogue");

  private GameState MyGameState;
  private double TutorialTimer;
  private int EntertainmentChangeThreshold;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
    MyGameState = GetNode<GameState>("/root/GameState");
    MyGameState.Load();
	  MyGameState.ResetForNewRun();
	  ++MyGameState.RunsMade;

    TutorialTimer = 3.4 + (Random.Shared.NextDouble() * 20);

    MyGameState.RunEndEvent += RunEndHandler;
    MyGameState.EntertainmentModifyEvent += ModifyEntertainmentHandler;
    MyGameState.CreatorCommentEvent += PlayCreatorDialogue;
    PlayCreatorDialogue("StartOfRun");
  }

  public override void _ExitTree() {
    base._ExitTree();
    MyGameState.RunEndEvent -= RunEndHandler;
    MyGameState.EntertainmentModifyEvent -= ModifyEntertainmentHandler;
    MyGameState.CreatorCommentEvent -= PlayCreatorDialogue;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
    CheckTutorials(delta);
  }

  private void CheckTutorials(double delta) {
    if (MyGameState.RunsMade > 1 || MyGameState.TutorialIndex > 0 || _player.InDialogue) return;
    TutorialTimer -= delta;
    if (TutorialTimer > 0) return;

    // reset timer
    TutorialTimer = 11 + (Random.Shared.NextDouble() * 20);

    switch (MyGameState.TutorialIndex++) {
      case 0:
        PlayCreatorDialogue("TutorialCamera");
        break;
      case 1:
        PlayCreatorDialogue("YesNoSlapComment");
        break;
    }
  }

  private void RunEndHandler() {
    MyGameState.Save();
    GetTree().ChangeSceneToFile(MenuScene);
  }

  private void ModifyEntertainmentHandler(int i) {
    _entertainmentBar.ModifyEntertainment(i);
    EntertainmentChangeThreshold += i;
    if (EntertainmentChangeThreshold > 15) {
      PlayCreatorDialogue("GettingInteresting");
      EntertainmentChangeThreshold = 0;
    }
    else if (EntertainmentChangeThreshold < -15) {
      PlayCreatorDialogue("GettingBored");
      EntertainmentChangeThreshold = 0;
    }
  }

  private void PlayCreatorDialogue(string startNode, bool blockPlayerInput = false) {
    var DialogueBalloon = DialogueScene.Instantiate();
    if (DialogueBalloon != null) {
      GetTree().CurrentScene.AddChild(DialogueBalloon);
      DialogueBalloon.Call("start", _creatorDialogue, startNode);
      DialogueBalloon.Call("setSpeaker", "F_LOW");
      if (blockPlayerInput) {
        _player.InDialogue = true;
        DialogueBalloon.TreeExiting += () => { _player.InDialogue = false; };
      }
    }
  }

}
