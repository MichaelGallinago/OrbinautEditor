using System.Linq;
using Godot;
using Godot.Collections;
using OrbinautEditor.General.Model;

namespace OrbinautEditor.PaletteEditor;

public partial class PaletteEditor : Control
{
    [Signal] public delegate void LoadPaletteEventHandler(string filePaths);
    [Signal] public delegate void LoadSpritesEventHandler(Array<string> filePaths);
    [Signal] public delegate void SavePaletteEventHandler(string filePath);
    
    private CustomFileDialog _fileDialog1;
    private CustomFileDialog _fileDialog2;

    public override void _Ready()
    {
        AddChild(_fileDialog1 = CreateCustomFileDialog());
        AddChild(_fileDialog2 = CreateCustomFileDialog());
    }

    private CustomFileDialog CreateCustomFileDialog()
    {
        return new CustomFileDialog
        {
            Unresizable = true,
            Size = GetTree().Root.Size,
            Access = FileDialog.AccessEnum.Filesystem,
            InitialPosition = Window.WindowInitialPosition.CenterScreenWithKeyboardFocus
        };
    }

    private void OnCreateSortedPalette()
    {
        _fileDialog1.OpenFile(ImageFile.Filters, FileDialog.FileModeEnum.OpenFile, 
            CreateSortedPalette, "Open Unsorted Palette", string.Empty);
    }

    private void CreateSortedPalette(string path)
    {
        Image palette = Image.LoadFromFile(path);
        Vector2I size = palette.GetSize();
        var colors = new System.Collections.Generic.Dictionary<uint, int>();
        for (var y = 0; y < size.Y; y++)
        {
            colors.TryAdd(palette.GetPixel(0, y).ToRgba32(), y);
        }

        var colorsRgba32 = colors.Keys.ToList();
        colorsRgba32.Sort();
        
        var sortedPalette = Image.Create(size.X, colorsRgba32.Count, false, Image.Format.Rgba8);

        for (var y = 0; y < colorsRgba32.Count; y++)
        {
            int originalY = colors[colorsRgba32[y]];
            for (var x = 0; x < size.X; x++)
            {
                sortedPalette.SetPixel(x, y, palette.GetPixel(x, originalY));
            }
        }

        _fileDialog2.OpenFile(ImageFile.Filters, FileDialog.FileModeEnum.SaveFile, 
            savePath => sortedPalette.SavePng(savePath), 
            "Save Sorted Palette", string.Empty);
    }

    private void OnLoadPalette()
    {
        _fileDialog1.OpenFile(ImageFile.Filters, FileDialog.FileModeEnum.OpenFile, 
            path => EmitSignal(SignalName.LoadPalette, path), 
            "Load Palette", string.Empty);
    }
        
    private void OnLoadSprites()
    {
        _fileDialog1.OpenFiles(ImageFile.Filters, FileDialog.FileModeEnum.OpenFiles, 
            paths => EmitSignal(SignalName.LoadSprites, paths), 
            "Load Sprites", string.Empty);
    }

    private void OnSavePalette()
    {
        _fileDialog1.OpenFile(ImageFile.Filters, FileDialog.FileModeEnum.SaveFile, 
            paths => EmitSignal(SignalName.SavePalette, paths), 
            "Save Palette", string.Empty);
    }
}