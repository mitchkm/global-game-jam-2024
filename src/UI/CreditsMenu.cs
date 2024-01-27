using Godot;
using System;

public partial class CreditsMenu : VBoxContainer
{

  [Signal]
  public delegate void GoToMainMenuHomeEventHandler();

  public Button BackToMenuButton { get; private set; } = default!;

  public override void _Ready()
  {
	BackToMenuButton = GetNode<Button>("%BackToMenuButton");

	BackToMenuButton.Pressed += OnBackToMenuButtonPressed;
  }

  private void OnBackToMenuButtonPressed()
  {
	EmitSignal(SignalName.GoToMainMenuHome);
  }

}
