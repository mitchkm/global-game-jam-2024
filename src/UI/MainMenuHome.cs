namespace YesNoSlap;

using Godot;
using System;

public partial class MainMenuHome : VBoxContainer
{

  [Signal]
  public delegate void GoToCreditsEventHandler();

  [Export]
  public string PlayButtonScene = "res://scenes/Game.tscn";


  public Button PlayButton { get; private set; } = default!;
  public Button CreditsButton { get; private set; } = default!;

  public Button QuitButton { get; private set; } = default!;

  public override void _Ready()
  {
	  PlayButton = GetNode<Button>("%PlayButton");
	  CreditsButton = GetNode<Button>("%CreditsButton");
	  QuitButton = GetNode<Button>("%QuitButton");

	  PlayButton.Pressed += OnPlayButtonPressed;
	  CreditsButton.Pressed += OnCreditsButtonPressed;
	  QuitButton.Pressed += OnQuitButtonPressed;
  }

  private void OnPlayButtonPressed()
  {
	  GetTree().ChangeSceneToFile(PlayButtonScene);
  }


  private void OnCreditsButtonPressed()
  {
	EmitSignal(SignalName.GoToCredits);
  }

  private void OnQuitButtonPressed()
  {
	  GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
  }

}
