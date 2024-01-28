namespace YesNoSlap.Interactable;

using Godot;

[GlobalClass]
public abstract partial class Interactable : Node {
  [Signal]
  public delegate void OnInteractedEventHandler(PlayerController player);

  public virtual void Interact(PlayerController player) => EmitSignal(SignalName.OnInteracted, player);

  public virtual void ToggleHighlight(bool toggle) { }
}
