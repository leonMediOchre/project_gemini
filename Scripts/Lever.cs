using System;
using Godot;

public partial class Lever : Sprite2D {

    /// <summary>
    /// Signal emitted on interaction with lever.
    /// </summary>
    [Signal]
    public delegate void ToggleEventHandler();

#pragma warning disable IDE0060
    public void On_Interact(PlayerController player) {
        EmitSignal("Toggle");
        FlipH = !FlipH;
    }
#pragma warning restore IDE0060

}