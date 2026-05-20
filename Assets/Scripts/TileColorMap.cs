using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Game/TileColorMap")]
public class TileColorMap : ScriptableObject
{
    [SerializeField] private List<TileColor> colorMap;
    private Dictionary<TileBase, Color> _colorDict;

    [Serializable]
    public class TileColor
    {
        public Tile tile;
        public Color color = Color.magenta;
    }

    public void ClearMap() => _colorDict = null;

    public Color GetColor(Tile tile)
    {
        if (!tile) return Color.clear;

        _colorDict ??= new Dictionary<TileBase, Color>();
        foreach (var mapping in colorMap)
            if (mapping.tile && !_colorDict.ContainsKey(mapping.tile))
                _colorDict.Add(mapping.tile, mapping.color);

        if (_colorDict.TryGetValue(tile, out var c)) return c;

        var t = new TileColor { tile = tile };
        _colorDict.Add(tile, t.color);
        colorMap.Add(t);

        return t.color;
    }
}