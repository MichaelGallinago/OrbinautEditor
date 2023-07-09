#nullable enable
using Godot;

public partial class Painter : Node2D
{
	public delegate void DrawHandler();
	public event DrawHandler DrawEvents
	{
		add
		{
			_draw += value;
			QueueRedraw();
		}
		remove
		{
			_draw -= value;
			QueueRedraw();
		}
	}

	private DrawHandler? _draw;

	public override void _Draw()
	{
		_draw?.Invoke();
	}
}
