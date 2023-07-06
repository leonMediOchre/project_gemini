using Godot;
using System;

public partial class PressurePlate : StaticBody2D {
    [Signal]
    public delegate void ToggleEventHandler();

    public void On_Body_Entered(Node2D body) {
        EmitSignal("Toggle");
    }

    public void On_Body_Exited(Node2D body) {
        EmitSignal("Toggle");
    }
}
