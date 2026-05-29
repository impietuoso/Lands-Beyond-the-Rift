using Data;
using Drafts.SaveData;
using SubSystems;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using UnityEngine;

public static class Game {
    public const int TileBuildSpeed = 300;

    private static GameSettings _settings;
    private static AudioManager _audio;
    private static GameObject _options;
    private static LoadController _load;
    private static CombatManager _combat;

    public static GameObject Options => Clone(ref _options, "Options Panel");
    public static AudioManager Audio => Clone(ref _audio);
    public static LoadController Loading => Clone(ref _load);
    public static CombatManager Combat => Clone(ref _combat);
    public static GameSettings Settings => Load(ref _settings);

    public static readonly SaveManager Save = new("Save", "default", new JsonFileParser());
    public static readonly LoadingTask LoadTasks = new();

    private static T Clone<T>(ref T clone, string name = null) where T : Object {
        if(clone) return clone;
        name ??= typeof(T).Name;
        clone = Resources.Load<T>(name).Clone();
        clone.name = name;
        Object.DontDestroyOnLoad(clone);
        return clone;
    }

    private static T Load<T>(ref T load, string name = null) where T : Object {
        if(load) return load;
        name ??= typeof(T).Name;
        load = Resources.Load<T>(name);
        return load;
    }

    //Sistema de Level up
}