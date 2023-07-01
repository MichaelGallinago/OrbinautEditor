using Godot;

public partial class ImageFactory : SubViewport
{
	public Painter Painter { get; } = new();

	public ImageFactory(Vector2I size)
	{
		Size = size;
		TransparentBg = true;
		RenderTargetUpdateMode = UpdateMode.Always;
		AddChild(Painter);
	}
}
