using UnityEngine;

namespace Rooms {
    public class MinimapBuilderTest : MonoBehaviour
    {
        public MinimapBuilder builder;
        public SpriteRenderer result;

        [ContextMenu("See Result")]
        public void SeeResult() {
            var ie = builder.BuildSprite();
            while (ie.MoveNext()) { }
            result.sprite = ie.Current;
        }
    }
}