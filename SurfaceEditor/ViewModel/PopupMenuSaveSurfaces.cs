using Godot;
using OrbinautEditor.General.Model;

namespace OrbinautEditor.SurfaceEditor.ViewModel;

public partial class PopupMenuSaveSurfaces : General.ViewModel.PopupMenuHandler
{
    public PopupMenuSaveSurfaces()
    {
        Name = "loadMenu";
        AddItem("Binary Surface", 0);
        AddItem("Image Surface", 1);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: OnBinarySurfacePressed(); break;
            case 1: OnImageSurfacePressed(); break;
        }
    }
    
    private static void OnBinarySurfacePressed()
    {
        SurfaceEditor.FileDialog.OpenFile(BinaryFile.Filters, FileDialog.FileModeEnum.SaveFile, 
            SurfaceEditor.SaveBinarySurface, "Save Binary Surface", string.Empty);
    }
    
    private static void OnImageSurfacePressed()
    {
        SurfaceEditor.FileDialog.OpenFile(ImageFile.Filters, FileDialog.FileModeEnum.SaveFile, 
            SurfaceEditor.SaveImageSurface, "Save Image Surface", string.Empty);
    }
}