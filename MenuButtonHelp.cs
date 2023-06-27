using Godot;
using System.Diagnostics;

public partial class MenuButtonHelp : MenuButton
{
	public override void _Ready()
	{
		GetPopup().IdPressed += OnItemPressed;
	}

	private static void OnItemPressed(long id)
	{
		switch (id)
		{
			case 0: OnDockOpened(); break;
		}
	}

	private static void OnDockOpened()
	{
		Process.Start(new ProcessStartInfo() 
		{
			FileName = "https://github.com/TrianglyRU/OrbinautFramework2",
			UseShellExecute = true
		});
	}
}
