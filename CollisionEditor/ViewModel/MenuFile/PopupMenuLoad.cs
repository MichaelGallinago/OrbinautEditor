using Godot;
using System;

public partial class PopupMenuLoad : PopupMenuHandler
{
    private CollisionEditorMainScreen screen;
    
    public PopupMenuLoad()
    {
        screen = (CollisionEditorMainScreen)GetTree().CurrentScene;
        
        Name = "loadMenu";
        AddItem("TileMap", 0);
        AddItem("AngleMap", 1);
    }
    
    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: OnTileMapPressed(); break;
            case 1: OnAngleMapPressed(); break;
        }
    }
    
    private void OnTileMapPressed()
    {
        var fileDialog = new FileDialog();
        fileDialog.AddFilter("*.png", "PNG");
        fileDialog.FileSelected += OnFileSelected;
    }

    private void OnFileSelected(string path)
    {
        GD.Print(path);
        screen.CreateTileSet(path);
    }

    private static void OnAngleMapPressed()
    {
        throw new NotImplementedException();
    }
}
