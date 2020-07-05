using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{

    public T _objectPrefab;

    private List<T> _objects = new List<T>();

    protected T GetObject()
    {
        foreach (T obj in _objects)
        {
            if (obj.gameObject.activeSelf == false)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        // No inactive object available. Create new object
        T objectCopy = Instantiate(_objectPrefab);
        objectCopy.transform.parent = this.gameObject.transform;

        _objects.Add(objectCopy);

        return objectCopy;
    }
}
