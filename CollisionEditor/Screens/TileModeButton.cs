using Godot;
using System;

public partial class TileModeButton : Button
{
    private CollisionEditorMain _screen;
    
    public override void _Ready()
    {
        _screen = CollisionEditorMain.Screen;
        Pressed += () => _screen.IsTileMode = true;
    }
}
