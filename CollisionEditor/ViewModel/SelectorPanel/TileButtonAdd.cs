using Godot;

public partial class TileButtonAdd : Button
{
	public override void _Ready()
	{
		CollisionEditorMain.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += CollisionEditorMain.AddTile;
	}
}
