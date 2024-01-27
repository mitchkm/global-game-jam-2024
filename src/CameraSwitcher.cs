using Godot;

namespace YesNoSlap;

public partial class CameraSwitcher : Node {
  [Export] private Node _northView;
  [Export] private Node _eastView;
  [Export] private Node _southView;
  [Export] private Node _westView;

  private int _currentIndex;
  private Node[] _views;

  // Method name For setting camera priority
  private const string SET_PRIORITY = "set_priority";

  public override void _Ready() {
    base._Ready();
    _views = new Node[] { _northView, _eastView, _southView, _westView };
    _currentIndex = 0;
  }

  public override void _Input(InputEvent @event) {
    base._Input(@event);
    if (@event.IsActionPressed("cam_rotate_left")) {
      NextViewLeft();
    }

    if (@event.IsActionPressed("cam_rotate_right")) {
      NextViewRight();
    }
  }

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
