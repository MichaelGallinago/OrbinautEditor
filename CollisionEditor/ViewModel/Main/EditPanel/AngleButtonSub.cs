using Godot;

public partial class AngleButtonSub : Button
{
	public override void _Ready()
	{
		CollisionEditor.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () => CollisionEditor.ChangeAngleBy(-1);
	}
}
