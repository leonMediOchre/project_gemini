using Godot;
using System;

public partial class Caterpillar : CharacterBody2D {

    [Export]
    public float speed = 300.0f;


    private readonly float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private Vector2 _gravity_vector = ProjectSettings.GetSetting("physics/2d/default_gravity_vector").AsVector2();

    private RayCast2D _wallRay;
    private RayCast2D _groundRay;

    public override void _Ready() {
        _wallRay = (RayCast2D)GetNode("WallRay");
        _groundRay = (RayCast2D)GetNode("GroundRay");
    }

    public override void _PhysicsProcess(double delta) {
        Velocity += _gravity_vector * _gravity * (float)delta;
        Velocity = new Vector2(speed, Velocity.Y);

        if (IsOnFloor() && (_wallRay.IsColliding() || !_groundRay.IsColliding())) {
            ApplyScale(new Vector2(-1, 1));
            speed *= -1;
        }

        MoveAndSlide();
    }
}
