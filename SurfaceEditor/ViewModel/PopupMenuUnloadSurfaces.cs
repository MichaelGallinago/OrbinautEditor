public partial class PopupMenuUnloadSurfaces : PopupMenuHandler
{
    public PopupMenuUnloadSurfaces()
    {
        Name = "unloadMenu";
        AddItem("Surface", 0);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: SurfaceEditor.CreateSurface(); break;
        }
    }
}
