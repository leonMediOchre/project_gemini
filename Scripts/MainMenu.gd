extends Node

@export var play_scene : Resource

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

func _on_play_button_pressed():
	get_tree().change_scene_to_file(play_scene.resource_path)


func _on_quit_button_pressed():
	get_tree().quit()
