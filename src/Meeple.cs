using Godot;

namespace YesNoSlap;

[Tool]
public partial class Meeple : CharacterBody3D {
  [ExportCategory("Appearance")]
  private Color _bodyColor = Colors.White;
  private Color _headColor = Colors.White;

  [Export]
  private Color BodyColor {
    get => _bodyColor;
    set {
      _bodyColor = value;
      SetColors();
    }
  }
  [Export]
  private Color HeadColor {
    get => _headColor;
    set {
      _headColor = value;
      SetColors();
    }
  }

  [ExportCategory("References")] [Export]
  private MeshInstance3D? _bodyInstance;

  [Export] private MeshInstance3D? _headInstance;

  public override void _Ready() => SetColors();

  private void SetColors() {
    if (_bodyInstance == null || _headInstance == null) {
      return;
    }

    var bodyMat = new StandardMaterial3D();
    var headMat = new StandardMaterial3D();
    bodyMat.AlbedoColor = _bodyColor;
    headMat.AlbedoColor = _headColor;

    _bodyInstance.MaterialOverride = bodyMat;
    _headInstance.MaterialOverride = headMat;
  }
}
