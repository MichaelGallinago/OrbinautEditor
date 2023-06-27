using Godot;
using System;

public partial class MenuButtonFile : MenuButton
{
	private PopupMenu loadMenu = new();
	private PopupMenu saveMenu = new();
	private PopupMenu unloadMenu = new();
	
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
		popup.AddSeparator();
		popup.AddItem("Exit");
	}
}
