using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.SelectorPanel;

public partial class TileButtonAdd : Button
{
	public override void _Ready()
	{
		CollisionEditor.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () => CollisionEditor.AddTile(CollisionEditor.TileIndex + (CollisionEditor.TileIndex == 0 ? 1 : 0));
	}
}