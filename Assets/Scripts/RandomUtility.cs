using System;
using System.Collections.Generic;
using System.Reflection;

public static class RandomUtility {
    public static T Random<T>(this IReadOnlyList<T> list) => list[UnityEngine.Random.Range(0, list.Count)];

    public static void ReflectSet<T>(this T obj, string field, object value) => ReflectSet<T>((object)obj, field, value);
    public static void ReflectSet<T>(this object obj, string field, object value) {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        var fieldInfo = typeof(T).GetField(field, flags);
        if (fieldInfo != null) {
            fieldInfo.SetValue(obj, value);
            return;
        }

        var propInfo = typeof(T).GetProperty(field, flags);
        if (propInfo != null && propInfo.CanWrite) {
            propInfo.SetValue(obj, value);
            return;
        }

        throw new MissingMemberException(typeof(T).Name, field);
    }
}