using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPool
{
    public GameObject prefab;

    public int prespawnCount;

    [NonSerialized]
    public List<GameObject> objects = new List<GameObject>();

#if UNITY_EDITOR
    private Transform parent;
#endif

    public GameObject Get()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].activeSelf == false)
                return objects[i];
        }

        return SpawnNewObject();
    }

    public void Prespawn()
    {
#if UNITY_EDITOR
        parent = new GameObject("EditorContainer_" + prefab.name).transform;
#endif

        for (int i = 0; i < prespawnCount; i++)
        {
            var gameObject = SpawnNewObject();
            gameObject.SetActive(false);
            objects.Add(gameObject);
        }
    }

    private GameObject SpawnNewObject()
    {
#if UNITY_EDITOR
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(parent, false);
        return go;
#else
        return GameObject.Instantiate(prefab);
#endif
    }
}
