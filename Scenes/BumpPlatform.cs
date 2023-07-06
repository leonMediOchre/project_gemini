using Godot;
using System;

public partial class BumpPlatform : CharacterBody2D
{
	[Export]
	public float speed = 200f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (MoveAndCollide(new Vector2(speed * (float)delta, 0)) != null)
			speed *= -1;
	}
}
