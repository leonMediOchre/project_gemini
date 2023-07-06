using Godot;
using System;

public partial class Toggleable : Node2D {
    private CollisionShape2D _collider;
    private bool _untoggle = false;
    private ushort _colCount = 0;

    public override void _Ready() {
        _collider = (CollisionShape2D)GetNode("CollisionShape2D");
    }

    public void On_Toggle() {
        if (_collider.Disabled && !_untoggle) {
            _untoggle = true;
            return;
        }
        if (_untoggle) _untoggle = false;
        Color mod = Modulate;
        mod.A = 0.5f;
        Modulate = mod;
        _collider.SetDeferred("disabled", true);
    }

    public override void _PhysicsProcess(double delta) {
        if (_untoggle && _colCount == 0) {
            Color mod = Modulate;
            mod.A = 1;
            Modulate = mod;
            _collider.SetDeferred("disabled", false);
            _untoggle = false;
        }
    }

    public void On_Body_Entered(Node2D body) {
        _colCount++;
    }

    public void On_Body_Exited(Node2D body) {
        _colCount--;
    }
}
