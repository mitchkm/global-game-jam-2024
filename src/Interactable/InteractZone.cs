using Godot;

namespace YesNoSlap.Interactable;

[GlobalClass]
public partial class InteractZone : Area3D {
  [Export] private Interactable _interactTarget;

  public override void _Ready() => BodyEntered += OnBodyEnteredHandler;

  private void OnBodyEnteredHandler(Node3D body) {
    if (body is PlayerController) {
      GD.Print("Interact with me!!!");
    }
  }
}
