using Godot;

namespace YesNoSlap;

[Tool]
public partial class Meeple : CharacterBody3D {
  [ExportCategory("Appearance")]
  private Color _bodyColor = Colors.White;
  private Color _headColor = Colors.White;
  private Color _leftArmColor = Colors.White;
  private Color _rightArmColor = Colors.White;

  [Export]
  private Color BodyColor {
    get => _bodyColor;
    set {
      _bodyColor = value;
      SetInstanceColor(_body, _bodyColor);
    }
  }
  [Export]
  private Color HeadColor {
    get => _headColor;
    set {
      _headColor = value;
      SetInstanceColor(_head, _headColor);
    }
  }

  [Export]
  private Color LeftArmColor {
    get => _leftArmColor;
    set {
      _leftArmColor = value;
      SetInstanceColor(_leftArm, _leftArmColor);
    }
  }
  [Export]
  private Color RightArmColor {
    get => _rightArmColor;
    set {
      _rightArmColor = value;
      SetInstanceColor(_rightArm, _rightArmColor);
    }
  }

  [ExportCategory("References")]
  [Export] private MeshInstance3D? _body;
  [Export] private MeshInstance3D? _head;
  [Export] private MeshInstance3D? _leftArm;
  [Export] private MeshInstance3D? _rightArm;

  public override void _Ready() => SetColors();

  private void SetColors() {
    SetInstanceColor(_body, _bodyColor);
    SetInstanceColor(_head, _headColor);
    SetInstanceColor(_leftArm, _leftArmColor);
    SetInstanceColor(_rightArm, _rightArmColor);
  }

  private static void SetInstanceColor(MeshInstance3D? instance, Color color) {
    if (instance == null) {
      return;
    }
    var newMaterial = instance.GetActiveMaterial(0).Duplicate() as StandardMaterial3D;
    newMaterial!.AlbedoColor = color;

    instance.MaterialOverride = newMaterial;
  }
}
