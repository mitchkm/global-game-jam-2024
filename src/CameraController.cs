using Godot;

namespace YesNoSlap;

[Tool]
public partial class CameraController : Camera3D {
  private float _defaultCamSize = 4;
  private float _minCamSize = 1;
  private float _maxCamSize = 8;
  private float _zoomSpeed = 0.5f;

  // Method name For setting camera priority
  private const string SET_PRIORITY = "set_priority";

  [Export]
  private float DefaultCamSize {
    get => _defaultCamSize;
    set {
      _defaultCamSize = Mathf.Clamp(value, MinCamSize, _maxCamSize);
      Size = _defaultCamSize;
    }
  }

  [Export]
  private float MinCamSize {
    get => _minCamSize;
    set => _minCamSize = value;
  }

  [Export]
  private float MaxCamSize {
    get => _maxCamSize;
    set => _maxCamSize = value;
  }

  [Export]
  private float ZoomSpeed {
    get => _zoomSpeed;
    set => _zoomSpeed = value;
  }

  [ExportCategory("Phantom Cameras")] [Export]
  private Node _northView;

  [Export] private Node _eastView;
  [Export] private Node _southView;
  [Export] private Node _westView;

  private float _currentCamSize;
  private int _currentIndex;
  private Node[] _views;

  public override void _Ready() {
    base._Ready();
    _views = new Node[] { _northView, _eastView, _southView, _westView };
    _currentIndex = 0;
    _currentCamSize = DefaultCamSize;
  }

  public override void _Input(InputEvent @event) {
    if (Engine.IsEditorHint()) {
      return;
    }

    base._Input(@event);
    if (@event.IsActionPressed("cam_rotate_left")) {
      NextViewLeft();
    }

    if (@event.IsActionPressed("cam_rotate_right")) {
      NextViewRight();
    }

    if (@event.IsActionPressed("zoom_up")) {
      AdjustZoom(-ZoomSpeed);
    }

    if (@event.IsActionPressed("zoom_down")) {
      AdjustZoom(ZoomSpeed);
    }
  }

  private void AdjustZoom(float amount) =>
    Size = Mathf.Clamp(Size + (amount), MinCamSize, MaxCamSize);

  private void SetNewViewPriority(int index) {
    for (var i = 0; i < _views.Length; i++) {
      _views[i].Call(SET_PRIORITY, i == index ? 1 : 0);
    }
  }

  private void NextViewLeft() => SetNewViewPriority(DecrementIndex());

  private void NextViewRight() => SetNewViewPriority(IncrementIndex());

  private int IncrementIndex() {
    _currentIndex++;
    if (_currentIndex >= _views.Length) {
      _currentIndex = 0;
    }

    return _currentIndex;
  }

  private int DecrementIndex() {
    _currentIndex--;
    if (_currentIndex <= -1) {
      _currentIndex = _views.Length - 1;
    }

    return _currentIndex;
  }
}
