using UnityEngine;

namespace TurnBasedRPG {
    public static class UnityExtensions {
        public static T Clone<T>(this T obj, Transform parent = null) where T : Object => Object.Instantiate(obj, parent);
        public static T Clone<T>(this T obj, Vector3 pos) where T : Object => Object.Instantiate(obj, pos, Quaternion.identity);
    }
}