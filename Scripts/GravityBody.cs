using Godot;

public partial class GravityBody : CharacterBody2D, IGravityObject {
    [Export]
    public bool Pushable = true;
    [Export]
    public float Friction = 200;

    private readonly float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private Vector2 _gravity_vector = ProjectSettings.GetSetting("physics/2d/default_gravity_vector").AsVector2();
    private bool _sticky;

    public override void _Ready() {
        GravitySystem.Register(this);
    }

    public void FlipHorizontal() {
        if (_sticky) return;
        _gravity_vector = GravitySystem.GravityVector;
    }

    public void FlipVertical() {
        if (_sticky) return;
        _gravity_vector = GravitySystem.GravityVector;
    }

    /// <summary>
    /// Sets the objects velocity, if pushable.
    /// </summary>
    /// <param name="vel">Velocity</param>
    public void Push(Vector2 vel) {
        if (Pushable) Velocity = vel;
    }

    public override void _PhysicsProcess(double delta) {
        // Apply gravity
        Velocity += _gravity_vector * _gravity * (float)delta;

        // Apply friction
        if (_gravity_vector.X == 0 && Mathf.Abs(Velocity.X) > 0)
            Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, Friction), 0);
        else if (_gravity_vector.Y == 0 && Mathf.Abs(Velocity.Y) > 0)
            Velocity = new Vector2(0, Mathf.MoveToward(Velocity.Y, 0, Friction));

        if (MoveAndSlide()) {
            CollisionObject2D col;
            int c = GetSlideCollisionCount();

            bool wasSticky = _sticky;
            _sticky = false;

            for (ushort i = 0; i < c; i++) {
                col = GetSlideCollision(i).GetCollider() as CollisionObject2D;
                if (col == null) continue;

                if (col.GetCollisionLayerValue(2)) _sticky = true;
            }

            if (wasSticky != _sticky)
                _gravity_vector = GravitySystem.GravityVector;

        }
    }

    public override void _ExitTree() {
        GravitySystem.Remove(this);
    }

}