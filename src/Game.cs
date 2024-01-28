using Godot;
using YesNoSlap;

public partial class Game : Node {

  [Export] private EntertainmentBar _entertainmentBar;

  public string MenuScene = "res://scenes/MainMenu.tscn";

  private GameState MyGameState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
    MyGameState = GetNode<GameState>("/root/GameState");
    MyGameState.Load();
	  MyGameState.ResetForNewRun();
	  ++MyGameState.RunsMade;

    MyGameState.RunEndEvent += RunEndHandler;
    MyGameState.EntertainmentModifyEvent += ModifyEntertainmentHandler;
  }

  public override void _ExitTree() {
    base._ExitTree();
    MyGameState.RunEndEvent -= RunEndHandler;
    MyGameState.EntertainmentModifyEvent -= ModifyEntertainmentHandler;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

  private void RunEndHandler() {
    MyGameState.Save();
    GetTree().ChangeSceneToFile(MenuScene);
  }

  private void ModifyEntertainmentHandler(int i) {
    _entertainmentBar.ModifyEntertainment(i);
  }

}
