using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Service Locatorʵ����Ϸ�п糡��ͨ��ģ��ķǵ����ַ���
/// </summary>
public class ModuleDispatcher
{
    /// <summary>
    /// ������ϣ��������Ϸ����ʱ��Ψһһ��������
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
    /// ��ʼ��ʱ����һ��GameObject�������ػ���Mono��ģ�顣
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
    /// ��ȡָ����ģ�顣
    /// </summary>
    /// <typeparam name="T">ģ������</typeparam>
    public T Get<T>() where T : IGameModule {
        string key = typeof(T).Name;
        if (!modules.ContainsKey(key)) {
            DevUtils.Log($"{key} is not activated in module settings.", "Module Dispatcher");
        }
        return (T)modules[key];
    }

    /// <summary>
    /// ע��ģ�鵽ģ��ء�
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
    /// ע�ᾲ̬ģ�顣
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Register<T>() where T : IGameModule, new() {
        Register<T>(new T());
    }

    /// <summary>
    /// ע�����Mono��ģ�顣
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterMono<T>() where T: MonoBehaviour, IGameModule {
        GameObject go = new GameObject(typeof(T).Name);
        T module = go.AddComponent<T>();
        go.transform.SetParent(gameObject.transform);
        Register<T>(module);
    }

    /// <summary>
    /// ��ģ�����ɾ���������Monoģ��Ҳ��ͬʱɾ����GameObject��
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
    /// Debug�ã���ȡ��ǰ��ģ����Ϣ��
    /// Ϊ�˰�ȫ�������һ��Clone���ڶ����塣
    /// </summary>
    public Dictionary<string, IGameModule> ReadModulesData() {
        return modules.ToDictionary(s => s.Key, s => s.Value);
    }
}
