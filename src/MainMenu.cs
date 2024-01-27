namespace YesNoSlap;

using System.Diagnostics;
using Godot;

public partial class MainMenu : Control
{
  [Export]
  public string PlayButtonScene = "res://scenes/Game.tscn";
  public Button PlayButton { get; private set; } = default!;

  public override void _Ready()
  => PlayButton = GetNode<Button>("%PlayButton");

  public void OnPlayButtonPressed()
  {
    GetTree().ChangeSceneToFile(PlayButtonScene);
  }
}
