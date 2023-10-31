using System;
using Godot;
using OrbinautEditor.General.ViewModel;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.MenuMode;

public partial class MenuButtonMode : MenuButtonHandler
{
    protected override void OnItemPressed(long id)
    {
        string path = id switch 
        {
            0 => "res://CollisionEditor/Screens/CollisionEditor.tscn",
            1 => "res://PaletteEditor/PaletteEditor.tscn",
            2 => "res://SurfaceEditor/SurfaceEditor.tscn",
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
        
        Error error = GetTree().ChangeSceneToFile(path);
        if (error == Error.Ok) return;
        throw new Exception(error.ToString());
    }
}