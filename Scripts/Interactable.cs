using Godot;
using System;

public partial class Interactable : Node {

    private Callable _interactHandler;
    private Node2D _InteractIndicator;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        _interactHandler = new(GetParent(), "On_Interact");
        _InteractIndicator = (Node2D)GetNode("../InteractIndicator");

    }

    public void On_Body_Entered(Node2D body) {
        if (body.HasSignal("Interact")) {
            body.Connect("Interact", _interactHandler);
            _InteractIndicator.Visible = true;
        }
    }

    public void On_Body_Exited(Node2D body) {
        if (body.HasSignal("Interact")) {
            body.Disconnect("Interact", _interactHandler);
            _InteractIndicator.Visible = false;
        }
    }
}
