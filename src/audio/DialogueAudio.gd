extends Control
class_name DialogueAudio

var speaker = "male"

# Called when the node enters the scene tree for the first time.
func _ready():
	Wwise.register_game_obj(self, self.name)
	Wwise.set_3d_position(self, get_global_transform())

func setSpeaker(spoker: String):
	speaker = spoker

func spokeHandler(_letter: String, _letter_index: int, _speed: float):
	match speaker:
		"F":
			Wwise.post_event_id(AK.EVENTS.PLAY_PLAY_RANDOM_LETTER_F, self)
		"F_LOW":
			Wwise.post_event_id(AK.EVENTS.PLAY_PLAY_RANDOM_LETTER_F_LOW, self)
		"F_HIGH":
			Wwise.post_event_id(AK.EVENTS.PLAY_PLAY_RANDOM_LETTER_F_HIGH, self)
		"M":
			Wwise.post_event_id(AK.EVENTS.PLAY_PLAY_RANDOM_LETTER_M, self)
		"M_LOW":
			Wwise.post_event_id(AK.EVENTS.PLAY_PLAY_RANDOM_LETTER_M_LOW, self)
		"M_HIGH":
			Wwise.post_event_id(AK.EVENTS.PLAY_PLAY_RANDOM_LETTER_M_HIGH, self)
		_:
			Wwise.post_event_id(AK.EVENTS.PLAY_PLAY_RANDOM_LETTER_F_LOW, self)
