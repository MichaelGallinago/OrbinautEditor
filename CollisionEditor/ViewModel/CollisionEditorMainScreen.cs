using Godot;
using System;

public partial class CollisionEditorMainScreen : Control
{
	private TileSet tileSet;
	
	public override void _Ready()
	{
		tileSet = new TileSet(this);
	}

	public override void _Process(double delta)
	{
		
	}

	public void CreateTileSet(string imagePath)
	{
		tileSet = new TileSet(this, imagePath);
	}
}
