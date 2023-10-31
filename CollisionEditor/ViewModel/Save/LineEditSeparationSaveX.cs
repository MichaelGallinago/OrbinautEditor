using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class LineEditSeparationSaveX : General.ViewModel.LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMap.Parameters.Separation = new Vector2I(int.Parse(Text), SaveTileMap.Parameters.Separation.Y);
    }
}