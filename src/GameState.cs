using Godot;

namespace YesNoSlap;

using Godot.Collections;

public partial class InteractionCounts : GodotObject {
  public int Yes;
  public int No;
  public int Slap;

  public void Reset() {
    Yes = 0;
    No = 0;
    Slap = 0;
  }

  public virtual Godot.Collections.Dictionary<string, Variant> GetSaveData() =>
    new()
    {
      { nameof(Yes), Yes },
      { nameof(No), No },
      { nameof(Slap), Slap },
    };

  public virtual void LoadSave(Godot.Collections.Dictionary<string, Variant> data) {
    Variant value;

    data.TryGetValue(nameof(Yes), out value);
    Yes = (int)value;

    data.TryGetValue(nameof(No), out value);
    No = (int)value;

    data.TryGetValue(nameof(Slap), out value);
    Slap = (int)value;
  }
}

public partial class Character : GodotObject  {
  public int Progress;
  public int TotalConvos;
  public int RunConvos;
  public InteractionCounts Total = new();
  public InteractionCounts Run = new();

  public virtual void RunReset() {
    Run.Reset();
    RunConvos = 0;
  }

  public virtual Godot.Collections.Dictionary<string, Variant> GetSaveData() =>
    new()
    {
      { nameof(Progress), Progress },
      { nameof(TotalConvos), TotalConvos },
      { nameof(Total), Total.GetSaveData() },
    };

  public virtual void LoadSave(Godot.Collections.Dictionary<string, Variant> data) {
    Variant value;

    data.TryGetValue(nameof(Progress), out value);
    Progress = (int)value;

    data.TryGetValue(nameof(TotalConvos), out value);
    TotalConvos = (int)value;

    data.TryGetValue(nameof(Total), out value);
    Total.LoadSave((Godot.Collections.Dictionary<string, Variant>) value);
  }

  public virtual void ProgressConvo() => ++Progress;

  public virtual void TallyConvo() {
    ++TotalConvos;
    ++RunConvos;
  }

  public virtual void TallyYes() {
    ++Total.Yes;
    ++Run.Yes;
  }

  public virtual void TallyNo() {
    ++Total.No;
    ++Run.No;
  }

  public virtual void TallySlap() {
    ++Total.Slap;
    ++Run.Slap;
  }
}

public partial class MatildaCharacter : Character {
}

public partial class SamsonCharacter : Character {
  public bool TrueName;

  public string Name => TrueName ? "Samson \"The Slap Master\"  Slappington" : "Samson";

  public override Dictionary<string, Variant> GetSaveData() {
    var data = base.GetSaveData();
    data.Add(nameof(TrueName), TrueName);
    return data;
  }

  public override void LoadSave(Dictionary<string, Variant> data) {
    base.LoadSave(data);

    Variant value;
    data.TryGetValue(nameof(TrueName), out value);
    TrueName = (bool)value;
  }
}

public partial class GameState : Node {
  public int RunsMade;
  public bool HasCornDog;

  // Characters
  public MatildaCharacter Matilda = new();
  public SamsonCharacter Samson = new();

  public void ResetForNewRun() {
    Matilda.RunReset();
    Samson.RunReset();
  }

  private Godot.Collections.Dictionary<string, Variant> GetSaveData() =>
    new()
    {
      { nameof(RunsMade), RunsMade },
      { nameof(HasCornDog), HasCornDog },
      { nameof(Matilda), Matilda.GetSaveData() },
      { nameof(Samson), Samson.GetSaveData() },
    };

  private void LoadSave(Godot.Collections.Dictionary<string, Variant> data) {
    Variant value;

    data.TryGetValue(nameof(RunsMade), out value);
    RunsMade = (int)value;

    data.TryGetValue(nameof(HasCornDog), out value);
    HasCornDog = (bool)value;

    data.TryGetValue(nameof(Matilda), out value);
    Matilda.LoadSave((Godot.Collections.Dictionary<string, Variant>) value);

    data.TryGetValue(nameof(Samson), out value);
    Samson.LoadSave((Godot.Collections.Dictionary<string, Variant>) value);
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
