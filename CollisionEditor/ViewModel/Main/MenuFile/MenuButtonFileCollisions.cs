using Godot;

public partial class MenuButtonFileCollisions : MenuButtonHandler
{
	public MenuButtonFileCollisions()
	{
		PopupMenu popup = GetPopup();
		
		popup.AddChild(new PopupMenuLoadCollisions());
		popup.AddSubmenuItem("Load", "loadMenu", 0);
		popup.AddChild(new PopupMenuSaveCollisions());
		popup.AddSubmenuItem("Save", "saveMenu", 1);
		popup.AddChild(new PopupMenuUnloadCollisions());
		popup.AddSubmenuItem("Unload", "unloadMenu", 2);
		popup.AddSeparator();
		popup.AddItem("Exit", 3);

		popup.SetItemDisabled(1, true);
		CollisionEditor.ActivityChangedEvents += isActive => popup.SetItemDisabled(1, !isActive);
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
