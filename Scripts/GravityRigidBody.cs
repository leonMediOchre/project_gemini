using Godot;

public partial class GravityRigidBody : RigidBody2D, IGravityObject {

    private readonly float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private Vector2 _gravity_vector = ProjectSettings.GetSetting("physics/2d/default_gravity_vector").AsVector2();

    public override void _Ready() {
        GravitySystem.Register(this);
    }

    public override void _PhysicsProcess(double delta) {
        LinearVelocity += _gravity_vector * _gravity * (float)delta;
    }

    public void FlipHorizontal() {
        _gravity_vector = _gravity_vector.Orthogonal();

    }

    public void FlipVertical() {
        _gravity_vector *= -1;
    }

    public override void _ExitTree() {
        GravitySystem.Remove(this);
    }
}