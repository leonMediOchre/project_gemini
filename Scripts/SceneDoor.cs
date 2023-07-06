using Godot;
using System;

public partial class SceneDoor : Node2D {
    [Export]
    public Resource scene;

    #pragma warning disable IDE0060
    public void On_Interact(PlayerController player) {
        GetTree().ChangeSceneToFile(scene.ResourcePath);
    }
    #pragma warning restore IDE0060
}
