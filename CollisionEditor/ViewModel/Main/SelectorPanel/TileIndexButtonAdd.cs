using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.SelectorPanel;

public partial class TileIndexButtonAdd : Button
{
	public override void _Ready()
	{
		CollisionEditor.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () => CollisionEditor.TileIndex++;
	}
}