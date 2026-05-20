using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Stack<T> _pool = new();

    public ComponentPool(T prefab, string parentName = null)
    {
        _prefab = prefab;
        _parent = new GameObject(parentName ?? typeof(T).Name + " Pool").transform;
        Object.DontDestroyOnLoad(_parent);
    }

    public void PreSwarm(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var clone = Object.Instantiate(_prefab, _parent);
            clone.gameObject.SetActive(false);
            _pool.Push(clone);
        }
    }

    public T Recycle()
    {
        if (_pool.Count == 0)
        {
            var clone = Object.Instantiate(_prefab, _parent);
            clone.gameObject.SetActive(true);
            return clone;
        }

        var recycled = _pool.Pop();
        recycled.gameObject.SetActive(true);
        return recycled;
    }

    public void Trash(T item)
    {
        item.gameObject.SetActive(false);
        _pool.Push(item);
    }
}