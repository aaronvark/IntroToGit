using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    /// <summary>
    /// Get the existing instance of this singleton object.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindFirstObjectByType<T>();

            if (_instance != null) 
                return _instance;
            
            GameObject singletonObject = new (typeof(T).Name);
            _instance = singletonObject.AddComponent<T>();
            
            DontDestroyOnLoad(singletonObject);

            return _instance;
        }
    }

    /// <summary>
    /// If there is an instance, this will destroy itself, otherwise it becomes the instance.
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        _instance = this as T;
        DontDestroyOnLoad(this.gameObject);
    }
}