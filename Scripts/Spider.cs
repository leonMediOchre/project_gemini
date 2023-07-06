using Godot;
using System;


public partial class Spider : CharacterBody2D {

    [Export]
    public float speed = 200f;
    private CollisionShape2D _collider;
    private RayCast2D _groundRay;
    private RayCast2D _wallRay;

    private readonly float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private Vector2 _gravity_vector = ProjectSettings.GetSetting("physics/2d/default_gravity_vector").AsVector2();

    private Vector2 _velocity;

    public override void _Ready() {
        _collider = GetNode("CollisionShape2D") as CollisionShape2D;
        _groundRay = GetNode("GroundRay") as RayCast2D;
        _wallRay = GetNode("WallRay") as RayCast2D;

        _velocity = new Vector2(speed, 0);
    }

    public override void _PhysicsProcess(double delta) {
        _velocity.Y += _gravity;

        if (IsOnFloor()) {
            _velocity.Y = 0;
            if (_wallRay.IsColliding()) {
                Rotate(-MathF.PI / 2);
                UpDirection = UpDirection.Rotated(-MathF.PI / 2);
            }
            if (!_groundRay.IsColliding()) {
                Rotate(MathF.PI / 2);
                UpDirection = UpDirection.Rotated(MathF.PI / 2);
            }
        }

        Velocity = _velocity.Rotated(Rotation);

        MoveAndSlide();
    }
}
