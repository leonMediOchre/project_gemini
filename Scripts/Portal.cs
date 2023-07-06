using Godot;
using System;

public partial class Portal : Node2D {

    [Export]
    public Portal destinationPortal;

    private Node2D _ignore = null;

    public void IgnoreBody(Node2D body) {
        _ignore = body;
    }

    public void On_Body_Entered(Node2D body) {
        if (destinationPortal == null) return;
        if (body == _ignore) {
            _ignore = null;
            return;
        }

        destinationPortal.IgnoreBody(body);

        body.Position = destinationPortal.Position;

    }
}
