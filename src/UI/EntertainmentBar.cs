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


  // public Texture2D superBoredFace = load("res://TempImages/ScientistFace/superBored.png");
  // public Texture2D boredFace = load("res://TempImages/ScientistFace/bored.png");
  // public Texture2D neutralFace = load("res://TempImages/ScientistFace/neutral.png");
  // public Texture2D happyFace = load("res://TempImages/ScientistFace/happy.png");
  // public Texture2D superHappyFace = load("res://TempImages/ScientistFace/superHappy.png");


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

	if (entertainment > 80)
	{
	  Meter.TintProgress = superHappyColor;
	}
	else if (entertainment > 60)
	{
	  Meter.TintProgress = happyColor;
	}
	else if (entertainment > 40)
	{
	  Meter.TintProgress = neutralColor;
	}
	else if (entertainment > 20)
	{
	  Meter.TintProgress = boredColor;
	}
	else
	{
	  Meter.TintProgress = superBoredColor;
	}

  }

}
