using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class DeleteContainerButton : Button
{
    public override void _Ready()
    {
        SaveTileMap.ExpertModeChangedEvents += isExpertMode => Visible = isExpertMode;
        Pressed += () =>
        {
            GetParent().QueueFree();
            SaveTileMap.UpdateImage();
        };
    }
}