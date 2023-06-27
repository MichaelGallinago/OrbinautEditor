using Godot;
using System;

public partial class TileButtonAdd : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	private static void OnPressed()
	{
		
	}
}
