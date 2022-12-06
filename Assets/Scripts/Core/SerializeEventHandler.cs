using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SerializeEventHandler : MonoBehaviour
{
    public bool saveData = true;

    public UserData userData;

    public float lastTimePlay;

    private void Awake()
    {
        var instance = FindObjectOfType<SerializeEventHandler>();
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        UserData.Load();
        userData = UserData.current;
        lastTimePlay = 0f;
        DontDestroyOnLoad(gameObject); 
    }

    public void OnApplicationQuit()
    {
       


        SaveData();
    }

    public void SaveData()
    {
#if !UNITY_EDITOR
        saveData = true;
#endif
        if (saveData)
        {
            UserData.Save();
            PlayerPrefs.Save();
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SerializeEventHandler))]
public class SerializeEventEditor : Editor
{
    private SerializeEventHandler handler;

    private void OnEnable()
    {
        handler = (SerializeEventHandler)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save"))
        {
            UserData.Save();
        }

        if (GUILayout.Button("Load"))
        {
            UserData.Load();
            handler.userData = UserData.current;
        }
    }
}
#endif
