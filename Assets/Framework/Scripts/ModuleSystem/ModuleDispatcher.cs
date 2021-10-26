using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDispatcher
{
    private ModuleDispatcher() { }

    private static ModuleDispatcher instance;
    private static GameObject gameObject;
    public static ModuleDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ModuleDispatcher();
                Init();
            }
            return instance;
        }
    }

    private static void Init()
    {
        if (gameObject == null) {
            gameObject = new GameObject("ModuleCenter");
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    private Dictionary<string, IGameModule> modules = new Dictionary<string, IGameModule>();

    public T Get<T>() where T : IGameModule {
        string key = typeof(T).Name;
        if (!modules.ContainsKey(key)) {
            DevUtils.Log($"{key} is not activated in module settings.", "Module Dispatcher");
        }
        return (T)modules[key];
    }

    public void Register<T>(T module) where T : IGameModule {
        string key = typeof(T).Name;
        if (modules.ContainsKey(key)) {
            DevUtils.Log($"{key} had been registered somewhere else.", "Module Dispatcher");
            return;
        }
        modules.Add(key, module);
    }

    public void Register<T>() where T : IGameModule, new() {
        Register<T>(new T());
    }

    public void RegisterMono<T>() where T: MonoBehaviour, IGameModule {
        T module = gameObject.AddComponent<T>();
        Register<T>(module);
    }

    public void Unregister<T>() where T : IGameModule {
        string key = typeof(T).Name;
        if (!modules.ContainsKey(key)) {
            DevUtils.Log($"{key} is not registered.", "Module Dispatcher");
            return;
        }
        IGameModule module = modules[key];
        if (module is MonoBehaviour) {
            GameObject.Destroy((MonoBehaviour)module);
        }
        modules.Remove(key);
    }


}
