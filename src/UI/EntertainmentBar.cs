using Godot;
using System;

public partial class EntertainmentBar : Control {
  private int entertainment = 50;

  public TextureRect Face { get; private set; } = default!;

  public TextureProgressBar Meter { get; private set; } = default!;


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
    Face = GetNode<TextureRect>("%ScientistFace");
    Meter = GetNode<TextureProgressBar>("%EntertainmentMeter");
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
  }

  public void SetEntertainment(int value) {
    entertainment = Math.Clamp(value, 0, 100);
    UpdateVisuals();
  }

  private void UpdateVisuals() {
    Meter.Value = entertainment;

    if (entertainment > 4 * (Meter.MaxValue / 5)) {
      Meter.TintProgress = superHappyColor;
      Face.Texture = happyFace;
      // Face.Texture = superHappyFace;
    }
    else if (entertainment > 3 * (Meter.MaxValue / 5)) {
      Meter.TintProgress = happyColor;
      Face.Texture = happyFace;
    }
    else if (entertainment > 2 * (Meter.MaxValue / 5)) {
      Meter.TintProgress = neutralColor;
      Face.Texture = neutralFace;
    }
    else if (entertainment > (Meter.MaxValue / 5)) {
      Meter.TintProgress = boredColor;
      Face.Texture = boredFace;
    }
    else {
      Meter.TintProgress = superBoredColor;
      Face.Texture = boredFace;
      // Face.Texture = superBoredFace;
    }
  }
}
