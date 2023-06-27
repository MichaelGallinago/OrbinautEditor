using Godot;

public abstract partial class PopupMenuHandler : PopupMenu
{
    public override void _Ready()
    {
        IdPressed += OnItemPressed;
    }

    protected abstract void OnItemPressed(long id);
}
