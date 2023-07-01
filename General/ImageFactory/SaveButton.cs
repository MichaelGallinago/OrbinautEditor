using Godot;
using System;

public partial class SaveButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		Image image = Image.LoadFromFile("res://water.png");
		var texture = ImageTexture.CreateFromImage(image);
		
		var imageFactory = new ImageFactory(new Vector2I(50, 50)); 
		AddChild(imageFactory);
		imageFactory.Painter.DrawEvents += () =>
		{
			imageFactory.Painter.DrawTexture(texture, new Vector2I(0, 0));
			RenderingServer.ForceDraw(); //It's the biggest piece of dog shit that I've ever heard
			imageFactory.GetTexture().GetImage().SavePng("MyImage.png");
			imageFactory.QueueFree();
		};
	}
}
