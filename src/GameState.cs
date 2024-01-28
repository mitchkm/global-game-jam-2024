using Godot;

namespace YesNoSlap;

public partial class GameState : Node {
  public int Variable1 = 0;
  public int Variable2 = 1;

  private Godot.Collections.Dictionary<string, Variant> GetSaveData() =>
    new()
    {
      { nameof(Variable1), Variable1 },
      { nameof(Variable2), Variable2 },
    };

  private void LoadSave(Godot.Collections.Dictionary<string, Variant> data) {
    Variant value;

    data.TryGetValue(nameof(Variable1), out value);
    Variable1 = (int)value;

    data.TryGetValue(nameof(Variable2), out value);
    Variable2 = (int)value;
  }

  public override void _Notification(int what)
  {
    if (what == NotificationWMCloseRequest) {
      Save();
      GetTree().Quit(); // default behavior
    }
  }

  public void Save() {
    using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);

    // Json provides a static method to serialized JSON string.
    var jsonString = Json.Stringify(GetSaveData());

    // Store the save dictionary as a new line in the save file.
    saveGame.StoreString(jsonString);
  }

  public void Load() {
    if (!FileAccess.FileExists("user://savegame.save"))
    {
      return; // Error! We don't have a save to load.
    }

    using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);

    var jsonString = saveGame.GetAsText();

    // Creates the helper class to interact with JSON
    var json = new Json();
    var parseResult = json.Parse(jsonString);
    if (parseResult != Error.Ok)
    {
      GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
      return;
    }

    // Get the data from the JSON object
    var data = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

    LoadSave(data);
  }

}
