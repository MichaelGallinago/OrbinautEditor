using Godot;

namespace OrbinautEditor.General.ViewModel;

public abstract partial class MenuButtonHandler : MenuButton
{
    protected MenuButtonHandler()
    {
        GetPopup().IdPressed += OnItemPressed;
    }
    
    protected abstract void OnItemPressed(long id);
}