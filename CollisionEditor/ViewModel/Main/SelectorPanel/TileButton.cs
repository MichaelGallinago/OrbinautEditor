using Godot;

public partial class TileButton : TextureButton
{
	public override void _Draw()
	{
		((ShaderMaterial)Material).SetShaderParameter("IsPressed", ButtonPressed);
	}
}
