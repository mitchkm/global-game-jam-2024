using Godot;

namespace YesNoSlap.Interactable;

[GlobalClass]
public abstract partial class InteractZone : Area3D, IInteractable {

  public override void _Ready() {
    BodyEntered += OnBodyEnteredHandler;
    BodyExited += OnBodyExitedHandler;
  }

  public override void _ExitTree() {
    BodyEntered -= OnBodyEnteredHandler;
    BodyExited -= OnBodyExitedHandler;
  }

  private void OnBodyEnteredHandler(Node3D body) {
    if (body is PlayerController player) {
      player.NotifyOfInteractTargetEnter(this);
    }
  }

  private void OnBodyExitedHandler(Node3D body) {
    if (body is PlayerController player) {
      player.NotifyOfInteractTargetExit(this);
    }
  }

  public abstract void Interact(PlayerController player);

  public abstract void ToggleHighlight(bool toggle);
}
