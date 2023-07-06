using Godot;
using System;

public partial class GravitySwitch : Node2D {
    [Export]
    public bool flipX;
    [Export]
    public bool flipY;

#pragma warning disable IDE0060
    public void On_Interact(PlayerController player) {
        if (flipX) GravitySystem.FlipHorizontal();
        if (flipY) GravitySystem.FlipVertical();
    }
#pragma warning restore IDE0060

}
