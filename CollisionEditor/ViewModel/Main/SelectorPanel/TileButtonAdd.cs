using Godot;

public partial class TileButtonAdd : Button
{
	public override void _Ready()
	{
		CollisionEditor.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += CollisionEditor.AddTile;
	}
}
