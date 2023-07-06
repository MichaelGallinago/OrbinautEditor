using Godot;
using System;

public partial class PopupMenuLoad : PopupMenuHandler
{
    private CollisionEditorMainScreen screen;
    private FileDialog fileDialog;
    
    public PopupMenuLoad()
    {
        Name = "loadMenu";
        AddItem("TileMap", 0);
        AddItem("AngleMap", 1);
    }
    
    public override void _Ready()
    {
        screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
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
        fileDialog = new FileDialog();
        fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
        fileDialog.AddFilter("*.png", "PNG");
        fileDialog.FileSelected += OnFileSelected;
        fileDialog.Visible = true;
        screen.AddChild(fileDialog);
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
