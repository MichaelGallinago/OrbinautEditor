using Godot;

public partial class AngleButtonSub : Button
{
	public override void _Ready()
	{
		CollisionEditorMain.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () => CollisionEditorMain.ChangeAngleBy(-1);
	}
}
