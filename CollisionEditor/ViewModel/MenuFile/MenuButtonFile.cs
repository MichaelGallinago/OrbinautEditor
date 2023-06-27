using Godot;
using System;

public partial class MenuButtonFile : MenuButtonHandler
{
	private PopupMenu loadMenu = new PopupMenuLoad();
	private PopupMenu saveMenu = new PopupMenuSave();
	private PopupMenu unloadMenu = new PopupMenuUnload();
	
	public MenuButtonFile()
	{
		var popup = GetPopup();

		popup.AddChild(loadMenu);
		popup.AddSubmenuItem("Load", "loadMenu", 0);
		popup.AddChild(saveMenu);
		popup.AddSubmenuItem("Save", "saveMenu", 1);
		popup.AddChild(unloadMenu);
		popup.AddSubmenuItem("Unload", "unloadMenu", 2);
		popup.AddSeparator();
		popup.AddItem("Exit", 3);
	}
	
	protected override void OnItemPressed(long id)
	{
		switch (id)
		{	
			case 0: LoadTileMap(); break;
			case 3: OnExitPressed(); break;
		}
	}
	private void LoadTileMap()
	{
		
	}
	
	private void OnExitPressed()
	{
		GetTree().Quit();
	}
}
