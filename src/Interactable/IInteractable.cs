namespace YesNoSlap.Interactable;

public interface IInteractable {

  public void Interact(PlayerController player);

  public void ToggleHighlight(bool toggle);
}
