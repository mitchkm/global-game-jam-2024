namespace YesNoSlap;

using System.Diagnostics;
using Godot;

public partial class MainMenu : Control
{
  public MainMenuHome MainMenuHome { get; private set; } = default!;
  public CreditsMenu CreditsMenu { get; private set; } = default!;

  //[Export]
  //public string PlayButtonScene = "res://scenes/TestGame.tscn";
  //public Button PlayButton { get; private set; } = default!;

  public override void _Ready()
  {
	MainMenuHome = GetNode<MainMenuHome>("%MainMenuHome");
	CreditsMenu = GetNode<CreditsMenu>("%CreditsMenu");

	MainMenuHome.GoToCredits += ShowCredits;
	CreditsMenu.GoToMainMenuHome += ShowMainMenuHome;
  }

  private void ShowCredits()
  {
	MainMenuHome.Visible = false;
	CreditsMenu.Visible = true;
  }

  private void ShowMainMenuHome()
  {
	MainMenuHome.Visible = true;
	CreditsMenu.Visible = false;
  }


}
