using Godot;

public partial class Hazard : Node {
    public void On_Body_Entered(Node2D body) {
        if (body is PlayerController player)
            player.Die();
    }
}