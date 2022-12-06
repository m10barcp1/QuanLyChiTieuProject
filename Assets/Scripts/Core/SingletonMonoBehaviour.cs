using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public bool dontDestroyOnLoad = true;

    protected bool isDuplicateIntance = false;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    var singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    instance.name = typeof(T).Name;                   
                }

                var self = instance as SingletonMonoBehaviour<T>;
                if (self.dontDestroyOnLoad)
                    DontDestroyOnLoad(self.gameObject);
            }

            return instance;
        }
    }

    public static bool IsInstanceExisting
    {
        get { return instance != null; }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;

            if (dontDestroyOnLoad) 
                DontDestroyOnLoad(instance.gameObject);
        }
        else if (instance.GetInstanceID() != GetInstanceID())
        {
            isDuplicateIntance = true;
            Destroy(gameObject);
        }
    }
}
