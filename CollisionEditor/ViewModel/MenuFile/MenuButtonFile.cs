using Godot;
using System;

public partial class MenuButtonFile : MenuButtonHandler
{
	public MenuButtonFile()
	{
		PopupMenu popup = GetPopup();

		popup.AddChild(new PopupMenuLoad());
		popup.AddSubmenuItem("Load", "loadMenu", 0);
		popup.AddChild(new PopupMenuSave());
		popup.AddSubmenuItem("Save", "saveMenu", 1);
		popup.AddChild(new PopupMenuUnload());
		popup.AddSubmenuItem("Unload", "unloadMenu", 2);
		popup.AddSeparator();
		popup.AddItem("Exit", 3);
	}
	
	protected override void OnItemPressed(long id)
	{
		switch (id)
		{
			case 3: OnExitPressed(); break;
		}
	}

	private void OnExitPressed()
	{
		GetTree().Quit();
	}
}
