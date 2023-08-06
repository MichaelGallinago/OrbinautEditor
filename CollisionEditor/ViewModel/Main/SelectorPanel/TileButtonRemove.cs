using Godot;

public partial class TileButtonRemove : Button
{
	public override void _Ready()
	{
		CollisionEditor.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () =>
		{
			if (CollisionEditor.TileSet.Tiles.Count <= 1) return;
			CollisionEditor.RemoveTile(CollisionEditor.TileIndex + (CollisionEditor.TileIndex == 0 ? 1 : 0));
		};
	}
}
