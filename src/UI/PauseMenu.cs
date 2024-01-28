using Godot;
using System;

public partial class PauseMenu : CenterContainer
{

  public string MenuScene = "res://scenes/MainMenu.tscn";

  public Button ResumeButton { get; private set; } = default!;

  public Button MenuButton { get; private set; } = default!;

  public Button QuitButton { get; private set; } = default!;

  private bool paused = false;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
	ResumeButton = GetNode<Button>("%ResumeButton");
	MenuButton = GetNode<Button>("%MenuButton");
	QuitButton = GetNode<Button>("%QuitButton");

	ResumeButton.Pressed += OnResumeButtonPressed;
	MenuButton.Pressed += OnMenuButtonPressed;
	QuitButton.Pressed += OnQuitButtonPressed;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
  }

  public override void _Input(InputEvent @event)
  {
	base._Input(@event);

	if (@event.IsActionPressed("pause"))
	{
	  PauseAction();
	}


  }

  private void OnResumeButtonPressed()
  {
	PauseAction();
  }

  private void OnQuitButtonPressed()
  {
	  GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
  }

  private void OnMenuButtonPressed()
  {
	PauseAction();
	GetTree().ChangeSceneToFile(MenuScene);
  }

  private void PauseAction()
  {
	paused = !paused;
	GetTree().Paused = paused;
	if (paused) Show();
	else Hide();
  }

}
