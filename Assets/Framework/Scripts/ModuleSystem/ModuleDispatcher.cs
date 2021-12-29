using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Service Locator实现游戏中跨场景通用模块的非单例分发。
/// </summary>
public class ModuleDispatcher
{
    /// <summary>
    /// 单例，希望这是游戏运行时的唯一一个单例。
    /// </summary>
    #region Singleton
    private ModuleDispatcher() { }

    private static ModuleDispatcher instance;
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

    #endregion

    private Dictionary<string, IGameModule> modules = new Dictionary<string, IGameModule>();
    private static GameObject gameObject;

    /// <summary>
    /// 初始化时创建一个GameObject用来承载基于Mono的模块。
    /// </summary>
    private static void Init()
    {
        if (gameObject == null) {
            gameObject = new GameObject("Module Center");
            gameObject.AddComponent<ModuleDispatcherInspector>();
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// 获取指定的模块。
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    public T Get<T>() where T : IGameModule {
        string key = typeof(T).Name;
        if (!modules.ContainsKey(key)) {
            DevUtils.Log($"{key} is not activated in module settings.", "Module Dispatcher");
        }
        return (T)modules[key];
    }

    /// <summary>
    /// 注册模块到模块池。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="module"></param>
    public void Register<T>(T module) where T : IGameModule {
        string key = typeof(T).Name;
        if (modules.ContainsKey(key)) {
            DevUtils.Log($"{key} had been registered somewhere else.", "Module Dispatcher");
            return;
        }
        modules.Add(key, module);
    }

    /// <summary>
    /// 注册静态模块。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Register<T>() where T : IGameModule, new() {
        Register<T>(new T());
    }

    /// <summary>
    /// 注册基于Mono的模块。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterMono<T>() where T: MonoBehaviour, IGameModule {
        GameObject go = new GameObject(typeof(T).Name);
        T module = go.AddComponent<T>();
        go.transform.SetParent(gameObject.transform);
        Register<T>(module);
    }

    /// <summary>
    /// 从模块池中删除，如果是Mono模块也会同时删除其GameObject。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Unregister<T>() where T : IGameModule {
        string key = typeof(T).Name;
        if (!modules.ContainsKey(key)) {
            DevUtils.Log($"{key} is not registered.", "Module Dispatcher");
            return;
        }
        IGameModule module = modules[key];
        if (module is MonoBehaviour) {
            GameObject.Destroy(((MonoBehaviour)module).gameObject);
        }
        modules.Remove(key);
    }

    /// <summary>
    /// Debug用，获取当前的模块信息。
    /// 为了安全起见做了一下Clone，掩耳盗铃。
    /// </summary>
    public Dictionary<string, IGameModule> ReadModulesData() {
        return modules.ToDictionary(s => s.Key, s => s.Value);
    }
}
