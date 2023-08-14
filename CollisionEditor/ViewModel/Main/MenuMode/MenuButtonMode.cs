using System;
using Godot;

public partial class MenuButtonMode : MenuButtonHandler
{
    protected override void OnItemPressed(long id)
    {
        string path = id switch 
        {
            0 => "res://CollisionEditor/Screens/CollisionEditor.tscn",
            1 => "res://SurfaceEditor/SurfaceEditor.tscn",
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
        
        Error error = GetTree().ChangeSceneToFile(path);
        if (error == Error.Ok) return;
        throw new Exception(error.ToString());
    }
}
