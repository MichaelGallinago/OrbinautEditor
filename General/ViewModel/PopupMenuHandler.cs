using Godot;

namespace OrbinautEditor.General.ViewModel;

public abstract partial class PopupMenuHandler : PopupMenu
{
    protected PopupMenuHandler()
    {
        IdPressed += OnItemPressed;
    }

    protected abstract void OnItemPressed(long id);
}