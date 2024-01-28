using Godot;
using YesNoSlap;

public partial class Game : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	  var MyGameState = GetNode<GameState>("/root/GameState");
    MyGameState.Load();
	  MyGameState.ResetForNewRun();
	  ++MyGameState.RunsMade;
  }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
