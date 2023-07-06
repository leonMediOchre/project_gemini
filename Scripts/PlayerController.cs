using Godot;


public partial class PlayerController : CharacterBody2D, IGravityObject {

    [Export]
    public float Speed = 300.0f;
    [Export]
    public float PushingForce = 200.0f;
    [Export]
    private bool InitFlippedX = false;
    [Export]
    private bool InitFlippedY = false;

    /// <summary>
    /// signal emitted on player interact.
    /// </summary>
    [Signal]
    public delegate void InteractEventHandler();

    private Sprite2D _sprite;
    private AnimationPlayer _animationPlayer;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    private readonly float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private Vector2 _gravity_vector = ProjectSettings.GetSetting("physics/2d/default_gravity_vector").AsVector2();

    private float _directionX = 0;

    private bool _sticky = false;
    public bool _flippedX;
    public bool _flippedY;
    private bool _wasOnFloor;
    private bool _landing;

    public override void _Ready() {
        GravitySystem.Register(this);
        
		_sprite = GetNode("Sprite2D") as Sprite2D;
        _animationPlayer = GetNode("AnimationPlayer") as AnimationPlayer;

		// Set player gravity and orientation based on initial flags
        if (InitFlippedX) FlipHorizontal();
        if (InitFlippedY) FlipVertical();
    }

    public override void _Process(double _delta) {
        _directionX = Input.GetAxis("move_left", "move_right");
        if (Input.IsActionJustPressed("interact")) EmitSignal("Interact", this);
    }

    public override void _PhysicsProcess(double delta) {
        // left-right velocity, relative to player orientation
        float velocityX;

        // Apply gravity.
        Velocity += _gravity_vector * _gravity * (float)delta;

        if (_landing && !_animationPlayer.IsPlaying()) _landing = false;

        /* ---------------------- Apply input if given ---------------------- */
        if (_directionX != 0) {
            if(!_landing) _animationPlayer.Play("walk");
            velocityX = _directionX * Speed;
            
			if (_directionX < 0) _sprite.FlipH = !_flippedX;
            else if (_directionX > 0) _sprite.FlipH = _flippedX;

            if (_flippedY) _sprite.FlipH = !_sprite.FlipH;
        } else // otherwise slow down till stop
            velocityX = Mathf.MoveToward(_flippedX ? Velocity.Y : Velocity.X, 0, Speed);
        /* ------------------------------------------------------------------ */

        /* --------- Set velocity appropriately to gravity direction -------- */
        if (_flippedX)
            Velocity = new Vector2(Velocity.X, velocityX);
        else
            Velocity = new Vector2(velocityX, Velocity.Y);
        /* ------------------------------------------------------------------ */

        if (!_landing) {
            if (!_wasOnFloor && IsOnFloor()) {
                _landing = true;
                _animationPlayer.Play("land");
            }
            else if (!IsOnFloor()) _animationPlayer.Play("fall");
            else if (velocityX == 0) _animationPlayer.Play("idle");
            _wasOnFloor = IsOnFloor();
        }

        /* ------------------- Move and handle collisions ------------------- */
        if (MoveAndSlide()) {
            CollisionObject2D col;
            int c = GetSlideCollisionCount();

            bool wasSticky = _sticky;
            _sticky = false;

            for (ushort i = 0; i < c; i++) {
                col = GetSlideCollision(i).GetCollider() as CollisionObject2D;
                if (col == null) continue;

                if (col.GetCollisionLayerValue(2)) _sticky = true;
                if (col.GetCollisionLayerValue(3)) Die();
                if (col is GravityBody gb) gb.Push(Velocity);
            }

            if (wasSticky != _sticky) {
                if (_flippedX != InitFlippedX ^ GravitySystem.GlobalFlippedX) FlipHorizontal();
                if (_flippedY != InitFlippedY ^ GravitySystem.GlobalFlippedY) FlipVertical();
            }
        }
        /* ------------------------------------------------------------------ */
    }
    public override void _ExitTree() {
        GravitySystem.Remove(this);
    }

	// flip player in X direction
    public void FlipHorizontal() {
        if (_sticky) return;

        _flippedX = !_flippedX;
        UpDirection = UpDirection.Orthogonal();
        _gravity_vector = -UpDirection;

        Rotate(_flippedX ? Mathf.DegToRad(-90) : Mathf.DegToRad(90));
    }

	// flip player in y direction
    public void FlipVertical() {
        if (_sticky) return;

        _flippedY = !_flippedY;
        UpDirection *= -1;
        _gravity_vector = -UpDirection;
        
        Rotate(Mathf.Pi);
    }

    /// <summary>
    /// Kills player.
    /// Triggers game over.
    /// </summary>
    public void Die() {
        GameController.GameOver();
    }
}
