namespace YesNoSlap.Interactable;

using Godot;

[GlobalClass]
public abstract partial class Interactable : Node {
  [Signal]
  public delegate void OnInteractedEventHandler();

  public virtual void Interact() => EmitSignal(SignalName.OnInteracted);
}
