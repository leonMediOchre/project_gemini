using Godot;
using System;

public partial class Spitter : StaticBody2D {
    [Export]
    public PackedScene sdprojectile;

    private Node2D _projectileSpawn;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        _projectileSpawn = GetNode("ProjectileSpawn") as Node2D;
    }

    public void On_Timer_Timeout() {
        Projectile proj = sdprojectile.Instantiate() as Projectile;
        proj.Position = _projectileSpawn.Position;
        AddChild(proj);
    }
}
