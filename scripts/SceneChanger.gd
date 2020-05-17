extends CanvasLayer

signal scene_changed()

onready var animation_player = $AnimationPlayer

func change_scene(scene_name, delay = 0.5):
	yield(get_tree().create_timer(delay), "timeout")
	animation_player.play("fade_in")
	yield(animation_player, "animation_finished")
	assert(get_tree().change_scene("scenes/%s.tscn" % scene_name) == OK)
	animation_player.play("fade_out")
	yield(animation_player, "animation_finished")
	emit_signal("scene_changed")