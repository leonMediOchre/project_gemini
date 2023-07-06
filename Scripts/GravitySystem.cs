using System.Collections.Generic;
using Godot;

/// <summary>
/// Handles global gravity changes.
/// <para> Communicates between gravity affected objects. </para>
/// <para> Use method <c>Register</c> to register IGravityObjects
/// with the system. </para>
/// </summary>
public static class GravitySystem {
    /// <summary>
    /// List of registered objects.
    /// </summary>
    public static List<IGravityObject> objects = new();


    /// <summary>
    /// Current global gravity vector.
    /// </summary>
    public static Vector2 GravityVector { get => s_gravityVector; }
    private static Vector2 s_gravityVector = Vector2.Down;

    /// <summary>
    /// True if global gravity is flipped in y direction.
    /// </summary>
    public static bool GlobalFlippedY { get { return GravityVector.X < 0 || GravityVector.Y < 0; } }

    /// <summary>
    /// True if global gravity is flipped in x direction.
    /// </summary>
    public static bool GlobalFlippedX { get { return GravityVector.X != 0; } }


    /// <summary>
    /// Register IGravityObject, so that it's gravity may be set by the system.
    /// </summary>
    /// <param name="o">Object to register.</param>
    public static void Register(IGravityObject o) {
        objects.Add(o);
    }

    /// <summary>
    /// Unregister object from the system.
    /// </summary>
    /// <param name="o">Object to remove.</param>
    public static void Remove(IGravityObject o) {
        objects.Remove(o);
    }

    /// <summary>
    /// Flip gravity of all registered objects in Y direction.
    /// </summary>
    public static void FlipVertical() {
        s_gravityVector *= -1;
        objects.ForEach(o => o.FlipVertical());
    }


    /// <summary>
    /// Flip gravity of all registered objects in X direction.
    /// </summary>
    public static void FlipHorizontal() {
        s_gravityVector = s_gravityVector.Orthogonal();
        objects.ForEach(o => o.FlipHorizontal());
    }

    public static void ResetGravity() {
        s_gravityVector = Vector2.Down;
    }
}