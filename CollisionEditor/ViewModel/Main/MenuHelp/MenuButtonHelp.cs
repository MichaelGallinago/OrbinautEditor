using System.Diagnostics;

public partial class MenuButtonHelp : MenuButtonHandler
{
	protected override void OnItemPressed(long id)
	{
		switch (id)
		{
			case 0: OnDockPressed(); break;
		}
	}

	private static void OnDockPressed()
	{
		Process.Start(new ProcessStartInfo() 
		{
			FileName = "https://github.com/TrianglyRU/OrbinautFramework2",
			UseShellExecute = true
		});
	}
}
