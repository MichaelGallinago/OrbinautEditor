using Godot;

public partial class AngleButtonAdd : Button
{
	public override void _Ready()
	{
		CollisionEditor.ActivityChangedEvents += isActive => Disabled = !isActive;
		CollisionEditor.TileIndexChangedEvents += () => Disabled = CollisionEditor.TileIndex == 0;
		Pressed += () => CollisionEditor.ChangeAngleBy(1);
	}
}
