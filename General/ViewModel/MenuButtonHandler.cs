using Godot;

public abstract partial class MenuButtonHandler : MenuButton
{
    public override void _Ready()
    {
        GetPopup().IdPressed += OnItemPressed;
    }
    
    protected abstract void OnItemPressed(long id);
}
