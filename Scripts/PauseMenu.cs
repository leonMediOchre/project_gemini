using Godot;

public partial class PauseMenu : Control {
    private bool _gameOver = false;
    private Label _label;
    private Button _resumeButton;

    public override void _Ready() {
        _label = (Label)GetNode("VBoxContainer/Label");
        _resumeButton = (Button)GetNode("VBoxContainer/ResumeButton");

        GameController.pauseMenu = this;
    }

    public override void _Process(double delta) {
        if (_gameOver) return;

        if (Input.IsActionJustPressed("pause")) {
            GetTree().Paused = !GetTree().Paused;
            Visible = GetTree().Paused;
        }
    }

    public void GameOver() {
        _label.Text = "GAME OVER";
        _resumeButton.Text = "Try again";
        _gameOver = true;
        Visible = true;
        GetTree().Paused = true;
    }

    public void On_Resume_Button_Pressed() {
        if (_gameOver) {
            _gameOver = false;
            _label.Text = "PAUSED";
            _resumeButton.Text = "Resume";
            GetTree().ReloadCurrentScene();
        }

        GetTree().Paused = false;
        Visible = false;
    }

    public void On_Quit_Button_Pressed() {
        GetTree().Quit();
    }

}