using Godot;

public partial class TileButtonRemove : Button
{
	public override void _Ready()
	{
		CollisionEditor.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += CollisionEditor.RemoveTile;
	}
}
