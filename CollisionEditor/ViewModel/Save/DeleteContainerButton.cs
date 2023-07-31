using Godot;

public partial class DeleteContainerButton : Button
{
    public override void _Ready()
    {
        Pressed += GetParent().QueueFree;
    }
}
