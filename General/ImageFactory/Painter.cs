#nullable enable
using Godot;

public partial class Painter : Node2D
{
	public delegate void DrawHandler();
	public event DrawHandler DrawEvents
	{
		add
		{
			draw += value;
			QueueRedraw();
		}
		remove
		{
			draw -= value;
			QueueRedraw();
		}
	}

	private DrawHandler? draw;

	public override void _Draw()
	{
		draw?.Invoke();
	}
}
