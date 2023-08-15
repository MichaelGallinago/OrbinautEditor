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
        popup.AddItem("Unload surface", 2);
        popup.AddSeparator();
        popup.AddItem("Exit", 3);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 2: SurfaceEditor.CreateSurface(); break;
            case 3: GetTree().Quit(); break;
        }
    }
}
