using Godot;
public partial class MenuButtonFileSurfaces : MenuButtonHandler
{
    public MenuButtonFileSurfaces()
    {
        PopupMenu popup = GetPopup();
		
        popup.AddChild(new PopupMenuLoadSurfaces());
        popup.AddSubmenuItem("Load", "loadMenu", 0);
        popup.AddChild(new PopupMenuSaveSurfaces());
        popup.AddSubmenuItem("Save", "saveMenu", 1);
        popup.AddChild(new PopupMenuUnloadSurfaces());
        popup.AddSubmenuItem("Unload", "unloadMenu", 2);
        popup.AddSeparator();
        popup.AddItem("Exit", 3);

        popup.SetItemDisabled(1, true);
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
