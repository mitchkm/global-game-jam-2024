extends Control


# Called when the node enters the scene tree for the first time.
func _ready():
	Wwise.register_game_obj(self, self.name)
	Wwise.set_3d_position(self, get_global_transform())

func spokeHandler(_letter: String, _letter_index: int, _speed: float):
	Wwise.post_event_id(AK.EVENTS.PLAY_RANDOM_LETTER, self)
