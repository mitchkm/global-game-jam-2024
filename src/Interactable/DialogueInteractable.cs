namespace YesNoSlap.Interactable;

using Godot;

[GlobalClass]
public partial class DialogueInteractable : InteractZone {
  [Export] private Resource? _dialogueResource;
  [Export] private string? _startNodeTitle = "start";
  [Export] private Node3D? _interactionHighlight;

  private static PackedScene DialogueScene = GD.Load<PackedScene>("res://scenes/hud/dialogue_balloon.tscn");

  public override void Interact(PlayerController player) {
    GD.Print(Owner.Name + " was interacted with");
    StartDialogue(player);
  }

  public override void ToggleHighlight(bool toggle) {
    if (_interactionHighlight != null) {
      _interactionHighlight.Visible = toggle;
    }
  }

  private void StartDialogue(PlayerController player) {
    if (_dialogueResource is null || _startNodeTitle is null) {
      return;
    }

    // Make dialogue hud and start it
    // MitchMiller: it's one time use, I don't want to fix that...
    var DialogueBalloon = DialogueScene.Instantiate();
    if (DialogueBalloon != null) {
      GetTree().CurrentScene.AddChild(DialogueBalloon);
      player.InDialogue = true;
      DialogueBalloon.Call("start", _dialogueResource, _startNodeTitle);
      DialogueBalloon.TreeExiting += () => { player.InDialogue = false; };
    }
  }
}
