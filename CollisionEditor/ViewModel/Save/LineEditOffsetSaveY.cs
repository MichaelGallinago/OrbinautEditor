using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class LineEditOffsetSaveY : General.ViewModel.LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMap.Parameters.Offset = new Vector2I(SaveTileMap.Parameters.Offset.X, int.Parse(Text));
    }
}