using Godot;
using System.Collections.Generic;
using System.Linq;


public partial class PathedPlatform : AnimatableBody2D {
    public List<Vector2> pathPoints = new();

    /// <summary>
    /// If true, after reaching the final point, the object will then go back
    /// the way it came, rather than moving immediately to the first point.
    /// </summary>
    [Export]
    public bool retraceSteps = false;
    [Export]
    public float _lerpWeight = 0.1f;
    [Export]
    public float _tolerance = 1f;

    private short _nextPos = 0;
    private short _increment = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        foreach (Node2D o in GetNode("Path").GetChildren().Cast<Node2D>())
            pathPoints.Add(o.GlobalPosition);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta) {
        float distance = Position.DistanceTo(pathPoints[_nextPos]);
		
        if (distance < _tolerance)
            _nextPos += _increment;
        if (_nextPos == pathPoints.Count)
            if (retraceSteps) {
                _increment = -1;
                _nextPos -= 2;
            } else _nextPos = 0;
        else if (_nextPos == -1) {
            _increment = 1;
            _nextPos = 1;
        }

        Position = Position.Lerp(pathPoints[_nextPos], _lerpWeight);
    }
}
