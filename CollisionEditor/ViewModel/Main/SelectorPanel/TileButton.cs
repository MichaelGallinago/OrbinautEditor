using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.SelectorPanel;

public partial class TileButton : TextureButton
{
	public override void _Draw()
	{
		((ShaderMaterial)Material).SetShaderParameter("IsPressed", ButtonPressed);
	}
}