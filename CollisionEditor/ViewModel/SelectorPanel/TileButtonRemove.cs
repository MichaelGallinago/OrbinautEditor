using Godot;

public partial class TileButtonRemove : Button
{
	public override void _Ready()
	{
		CollisionEditorMain.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += CollisionEditorMain.RemoveTile;
	}
}
