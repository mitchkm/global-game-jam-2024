using Godot;
using System;

public partial class EntertainmentBar : HBoxContainer
{

  private int entertainment = 50;

  public TextureRect Face { get; private set; } = default!;

  public TextureProgressBar Meter { get; private set; } = default!;


  public Color superBoredColor = new Color(0.725f, 0.204f, 0, 1);
  public Color boredColor = new Color(0.596f, 0.345f, 0, 1);
  public Color neutralColor = new Color(0.482f, 0.412f, 0, 1);
  public Color happyColor = new Color(0.314f, 0.396f, 0, 1);
  public Color superHappyColor = new Color(0.102f, 0.435f, 0, 1);


  public Texture2D superBoredFace = (Texture2D)GD.Load("res://TempImages/ScientistFace/superBored.png");
  public Texture2D boredFace = (Texture2D)GD.Load("res://TempImages/ScientistFace/slightlyBored.png");
  public Texture2D neutralFace = (Texture2D)GD.Load("res://TempImages/ScientistFace/neutral.png");
  public Texture2D happyFace = (Texture2D)GD.Load("res://TempImages/ScientistFace/slightlyHappy.png");
  public Texture2D superHappyFace = (Texture2D)GD.Load("res://TempImages/ScientistFace/superHappy.png");


  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
	Face = GetNode<TextureRect>("%ScientistFace");
	Meter = GetNode<TextureProgressBar>("%EntertainmentMeter");
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
  }

  public override void _Input(InputEvent @event)
  {
	base._Input(@event);

	if (@event.IsActionPressed("temp_add_entertainment"))
	{
	  IncrementEntertainment();
	}

	if (@event.IsActionPressed("temp_remove_entertainment"))
	{
	  DecrementEntertainment();
	}

  }

  private void IncrementEntertainment()
  {
	entertainment++;
	UpdateVisuals();
  }

  private void DecrementEntertainment()
  {
	entertainment--;
	UpdateVisuals();
  }

  private void SetEntertainment(int value)
  {
	if (value < 0)
	{
	  entertainment = 0;
	  return;
	}
	if (value > 100)
	{
	  entertainment = 100;
	  return;
	}
	entertainment = value;
	UpdateVisuals();
  }

  private void UpdateVisuals()
  {
	Meter.Value = entertainment;

	if (entertainment > 4 * (Meter.MaxValue / 5))
	{
	  Meter.TintProgress = superHappyColor;
	  Face.Texture = superHappyFace;
	}
	else if (entertainment > 3 * (Meter.MaxValue / 5))
	{
	  Meter.TintProgress = happyColor;
	  Face.Texture = happyFace;
	}
	else if (entertainment > 2 * (Meter.MaxValue / 5))
	{
	  Meter.TintProgress = neutralColor;
	  Face.Texture = neutralFace;
	}
	else if (entertainment > (Meter.MaxValue / 5))
	{
	  Meter.TintProgress = boredColor;
	  Face.Texture = boredFace;
	}
	else
	{
	  Meter.TintProgress = superBoredColor;
	  Face.Texture = superBoredFace;
	}

  }

}
