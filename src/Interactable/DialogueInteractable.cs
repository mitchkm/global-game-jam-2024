namespace YesNoSlap.Interactable;

using System.Diagnostics;
using Godot;
using DialogueManagerRuntime;

[GlobalClass]
public partial class DialogueInteractable : Interactable {
  [Export] private Resource? _dialogueResource;
  [Export] private string? _startNodeTitle = "start";

  public override void Interact() {
    base.Interact();
    GD.Print(Owner.Name + " was interacted with");
    StartDialogue();
  }

  private void StartDialogue() {
    if (_dialogueResource is null || _startNodeTitle is null) {
      return;
    }

    DialogueManager.ShowExampleDialogueBalloon(_dialogueResource, _startNodeTitle);
  }
}
