using Godot;

namespace OrbinautEditor.PaletteEditor;

public partial class MenuButtonFilePalette : General.ViewModel.MenuButtonHandler
{
    [Signal] public delegate void LoadPaletteEventHandler();
    [Signal] public delegate void LoadSpritesEventHandler();
    [Signal] public delegate void SavePaletteEventHandler();
    [Signal] public delegate void UnloadPaletteEventHandler();
    [Signal] public delegate void CreateSortedPaletteEventHandler();
    
    public MenuButtonFilePalette()
    {
        PopupMenu popup = GetPopup();
		    
        popup.AddItem("Load Palette", 0);
        popup.AddItem("Load Sprites", 1);
        popup.AddItem("Save Palette", 2);
        popup.AddItem("Unload Palette", 3);
        popup.AddItem("Create Sorted Palette", 4);
        popup.AddSeparator();
        popup.AddItem("Exit", 5);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: EmitSignal(SignalName.LoadPalette); break;
            case 1: EmitSignal(SignalName.LoadSprites); break;
            case 2: EmitSignal(SignalName.SavePalette); break;
            case 3: EmitSignal(SignalName.UnloadPalette); break;
            case 4: EmitSignal(SignalName.CreateSortedPalette); break;
            case 5: GetTree().Quit(); break;
        }
    }
}