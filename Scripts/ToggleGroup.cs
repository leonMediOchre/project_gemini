using Godot;
using System;
using System.Linq;


public partial class ToggleGroup : Node {
    public void On_Toggle() {
        foreach (Toggleable t in GetChildren().Cast<Toggleable>())
            t.On_Toggle();
    }
}
