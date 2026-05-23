using Controllers;
using Drafts.SaveData;
using UnityEngine;

public static class Game {
    private static AudioManager _audio;
    private static GameObject _options;
    private static LoadController _load;

    public static AudioManager Audio => _audio ??= Load<AudioManager>();
    public static GameObject Options => _options ??= Load<GameObject>("Options Panel");
    public static LoadController Loading => _load ??= Load<LoadController>();

    public static SaveManager Save = new("Save", "default", new JsonFileParser());

    private static T Load<T>(string name = null) where T : Object {
        name ??= typeof(T).Name;
        var clone = Resources.Load<T>(name);
        clone.name = name;
        Object.DontDestroyOnLoad(clone);
        return clone;
    }

    //Sistema de Level up
}