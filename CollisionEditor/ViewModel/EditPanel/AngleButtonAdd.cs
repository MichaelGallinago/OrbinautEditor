using Godot;

public partial class AngleButtonAdd : Button
{
	public override void _Ready()
	{
		CollisionEditorMain.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () => CollisionEditorMain.ChangeAngleBy(1);
	}
}
