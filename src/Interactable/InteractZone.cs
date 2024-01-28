using Godot;

namespace YesNoSlap.Interactable;

[GlobalClass]
public partial class InteractZone : Area3D {
  [Export] private Interactable? _interactTarget;

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
      if (_interactTarget != null) {
        player.NotifyOfInteractTargetEnter(_interactTarget);
      }
    }
  }

  private void OnBodyExitedHandler(Node3D body) {
    if (body is PlayerController player) {
      if (_interactTarget != null) {
        player.NotifyOfInteractTargetExit(_interactTarget);
      }
    }
  }
}
