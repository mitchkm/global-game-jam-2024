using Godot;
using System;

public partial class EntertainmentBar : Control {
  private int entertainment = 50;

  [Export] private TextureRect _face;

  [Export] private TextureProgressBar _meter;


  public Color superBoredColor = new Color(0.725f, 0.204f, 0, 1);
  public Color boredColor = new Color(0.596f, 0.345f, 0, 1);
  public Color neutralColor = new Color(0.482f, 0.412f, 0, 1);
  public Color happyColor = new Color(0.314f, 0.396f, 0, 1);
  public Color superHappyColor = new Color(0.102f, 0.435f, 0, 1);


  // public Texture2D superBoredFace = (Texture2D)GD.Load("res://images/creator/creator-superbored.png");
  public Texture2D boredFace = (Texture2D)GD.Load("res://images/creator/creator-bored.png");
  public Texture2D neutralFace = (Texture2D)GD.Load("res://images/creator/creator-neutral.png");
  public Texture2D happyFace = (Texture2D)GD.Load("res://images/creator/creator-happy.png");
  // public Texture2D superHappyFace = (Texture2D)GD.Load("res://images/creator/creator-superhappy.png");


  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    UpdateVisuals();
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta) {
  }

  public override void _Input(InputEvent @event) {
    base._Input(@event);
    if (@event.IsActionPressed("temp_add_entertainment")) {
      ModifyEntertainment(1);
    }

    if (@event.IsActionPressed("temp_remove_entertainment")) {
      ModifyEntertainment(-1);
    }
  }

  public void ModifyEntertainment(int delta) {
    entertainment = Math.Clamp(entertainment + delta, 0, 100);
    UpdateVisuals();
  }

  public void SetEntertainment(int value) {
    entertainment = Math.Clamp(value, 0, 100);
    UpdateVisuals();
  }

  private void UpdateVisuals() {
    _meter.Value = entertainment;

    if (entertainment > 4 * (_meter.MaxValue / 5)) {
      _meter.TintProgress = superHappyColor;
      _face.Texture = happyFace;
      // Face.Texture = superHappyFace;
    }
    else if (entertainment > 3 * (_meter.MaxValue / 5)) {
      _meter.TintProgress = happyColor;
      _face.Texture = happyFace;
    }
    else if (entertainment > 2 * (_meter.MaxValue / 5)) {
      _meter.TintProgress = neutralColor;
      _face.Texture = neutralFace;
    }
    else if (entertainment > (_meter.MaxValue / 5)) {
      _meter.TintProgress = boredColor;
      _face.Texture = boredFace;
    }
    else {
      _meter.TintProgress = superBoredColor;
      _face.Texture = boredFace;
      // Face.Texture = superBoredFace;
    }
  }
}
