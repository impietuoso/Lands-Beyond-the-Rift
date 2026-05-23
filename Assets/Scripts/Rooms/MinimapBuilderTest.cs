using UnityEngine;

namespace Rooms {
    public class MinimapBuilderTest : MonoBehaviour
    {
        public MinimapBuilder builder;
        public SpriteRenderer result;

        [ContextMenu("See Result")]
        public void SeeResult() => result.sprite = builder.BuildSprite();
    }
}