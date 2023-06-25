using Godot;
using System;

public partial class MenuButtonFile : MenuButton
{
	private PopupMenu loadMenu = new();
	private PopupMenu saveMenu = new();
	private PopupMenu unloadMenu = new();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var popup = GetPopup();
		
		loadMenu.Name = "loadMenu";
		loadMenu.AddItem("TileMap");
		loadMenu.AddItem("AngleMap");
		
		saveMenu.Name = "saveMenu";
		saveMenu.AddItem("All");
		saveMenu.AddItem("AngleMap");
		saveMenu.AddItem("HeightMap");
		saveMenu.AddItem("WidthMap");
		saveMenu.AddItem("TileMap");

		unloadMenu.Name = "unloadMenu";
		unloadMenu.AddItem("All");
		unloadMenu.AddItem("TileMap");
		unloadMenu.AddItem("AngleMap");

		popup.AddChild(loadMenu);
		popup.AddSubmenuItem("Load", "loadMenu");
		popup.AddChild(saveMenu);
		popup.AddSubmenuItem("Save", "saveMenu");
		popup.AddChild(unloadMenu);
		popup.AddSubmenuItem("Unload", "unloadMenu");
		popup.AddItem("Exit");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
