using Godot;

namespace OrbinautEditor.PaletteEditor;

public partial class MenuButtonFilePalette : General.ViewModel.MenuButtonHandler
{
    [Signal] public delegate void LoadSpritesEventHandler();
    [Signal] public delegate void SavePaletteEventHandler();
    [Signal] public delegate void CreateSortedPaletteEventHandler();
    
    public MenuButtonFilePalette()
    {
        PopupMenu popup = GetPopup();
		    
        popup.AddItem("Load Sprites", 0);
        popup.AddItem("Save Palette", 1);
        popup.AddItem("Create Sorted Palette", 2);
        popup.AddSeparator();
        popup.AddItem("Exit", 3);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: EmitSignal(SignalName.LoadSprites); break;
            case 1: EmitSignal(SignalName.SavePalette); break;
            case 2: EmitSignal(SignalName.CreateSortedPalette); break;
            case 3: GetTree().Quit(); break;
        }
    }
}