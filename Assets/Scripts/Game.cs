using Controllers;
using Drafts.SaveData;
using TurnBasedRPG;
using UnityEngine;
using TurnBasedRPG;

public static class Game {
    public const int TileBuildSpeed = 100;

    private static AudioManager _audio;
    private static GameObject _options;
    private static LoadController _load;
    private static CombatManager _combat;

    public static GameObject Options => _options ? _options : _options = Load<GameObject>("Options Panel");
    public static AudioManager Audio => _audio ? _audio : _audio = Load<AudioManager>();
    public static LoadController Loading => _load ? _load : _load = Load<LoadController>();
    public static CombatManager Combat => _combat ? _combat : _combat = Load<CombatManager>();

    public static SaveManager Save = new("Save", "default", new JsonFileParser());

    private static T Load<T>(string name = null) where T : Object {
        name ??= typeof(T).Name;
        var clone = Resources.Load<T>(name).Clone();
        clone.name = name;
        Object.DontDestroyOnLoad(clone);
        return clone;
    }

    //Sistema de Level up
}