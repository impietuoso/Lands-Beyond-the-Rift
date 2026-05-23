using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rooms {
    public class MinimapBuilder : MonoBehaviour
    {
        public TileColorMap colorMap;
        public Tilemap tilemap;

        public Sprite BuildSprite()
        {
            colorMap.ClearMap();

            var bounds = tilemap.cellBounds;
            var width = bounds.size.y;
            var height = bounds.size.x;
            var texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;

            for (var cellY = bounds.yMin; cellY < bounds.yMax; cellY++)
            for (var cellX = bounds.xMin; cellX < bounds.xMax; cellX++)
            {
                var pos = new Vector3Int(cellX, cellY, 0);
                var tile = (Tile)tilemap.GetTile(pos);
                var color = colorMap.GetColor(tile);
                var texX = cellX - bounds.xMin;
                var texY = cellY - bounds.yMin;

                texture.SetPixel(texY, texX, color);
            }

            texture.Apply();
            var rect = new Rect(0, 0, width, height);
            var sprite = Sprite.Create(texture, rect, Vector2.one / 2);
            return sprite;
        }
    }
}