using Godot;
using System;

public partial class Projectile : AnimatableBody2D {
    [Export]
    public float speed = 500;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        if (MoveAndCollide(new Vector2(speed * (float)delta, 0)) != null)
            QueueFree();
    }
}
