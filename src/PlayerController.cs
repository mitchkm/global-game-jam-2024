using Godot;

namespace YesNoSlap;

using Interactable;

[Tool]
public partial class PlayerController : Meeple {
  [Export] private float _speed = 5.0f;
  [Export] private Node3D _camera;

  public bool InDialogue { get; set; }

  // Get the gravity from the project settings to be synced with RigidBody nodes.
  private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
  private IInteractable? _interactTarget;

  public override void _Input(InputEvent @event) {
    if (Engine.IsEditorHint()) {
      return;
    }

    if (@event.IsActionPressed("interact")) {
      InteractWithTarget();
    }
  }

  public override void _PhysicsProcess(double delta) {
    if (Engine.IsEditorHint()) {
      return;
    }

    var velocity = Velocity;

    // Add the gravity.
    if (!IsOnFloor()) {
      velocity.Y -= _gravity * (float)delta;
    }

    // Get the input direction and handle the movement/deceleration.
    // As good practice, you should replace UI actions with custom gameplay actions.
    var inputDir = InDialogue ? Vector2.Zero : Input.GetVector("move_left", "move_right", "move_up", "move_down");
    var forward = Vector3.Back.Rotated(Vector3.Up,_camera.Rotation.Y).Normalized();
    var right = Vector3.Right.Rotated(Vector3.Up,_camera.Rotation.Y).Normalized();
    var direction = (forward * inputDir.Y) + (right * inputDir.X);

    if (direction != Vector3.Zero) {
      velocity.X = direction.X * _speed;
      velocity.Z = direction.Z * _speed;
    }
    else {
      velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
      velocity.Z = Mathf.MoveToward(Velocity.Z, 0, _speed);
    }

    Velocity = velocity;
    MoveAndSlide();
  }

  private void InteractWithTarget() => _interactTarget?.Interact(this);

  public void NotifyOfInteractTargetEnter(IInteractable interactableTarget) {
    if (_interactTarget == null) {
      _interactTarget = interactableTarget;
      _interactTarget.ToggleHighlight(true);
    }
  }

  public void NotifyOfInteractTargetExit(IInteractable interactableTarget) {
    if (_interactTarget == interactableTarget) {
      _interactTarget.ToggleHighlight(false);
      _interactTarget = null;
    }
  }
}
