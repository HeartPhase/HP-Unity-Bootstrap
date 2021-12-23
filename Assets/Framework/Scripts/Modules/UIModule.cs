using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIģ�顣���Ƴ����д��ڡ�����ȵ���ʾ��
/// </summary>
public class UIModule : MonoBehaviour, IGameModule
{
    /// <summary>
    /// ע��ģ�顣
    /// </summary>
    public static void Init()
    {
        ModuleDispatcher.Instance.RegisterMono<UIModule>();
        DevUtils.Log("Inited", "UIModule");
    }

    public static GameObject mainCanvasGO;
    public static MainCanvas mainCanvas;
    private static Transform contentParent;

    private static Dictionary<string, GameObject> cachedWindows;
    private static Dictionary<string, GameObject> cachedOverlays;

    [Obsolete] private static Dictionary<string, GameObject> cachedUIs;

    private UISettings uiSettings;

    /// <summary>
    /// ������Canvas��UI����,��ȡUIģ�����á�
    /// </summary>
    private void Awake()
    {
        CreateMainCanvas();
        DevUtils.Log("Main canvas created", "UIModule");
        cachedWindows = new Dictionary<string, GameObject>();
        cachedOverlays = new Dictionary<string, GameObject>();
        uiSettings = ModuleDispatcher.Instance.Get<FrameworkSettings>().uiSettings;
    }

    /// <summary>
    /// ������Canvas��
    /// ���·����д���ģ���Ϊ����Ҫ�Ķ���
    /// </summary>
    public static void CreateMainCanvas() {
        mainCanvasGO = Instantiate(Resources.Load<GameObject>("Components/MainCanvas/MainCanvas"));
        mainCanvasGO.name = "Main Canvas";
        DontDestroyOnLoad(mainCanvasGO);
        mainCanvas = mainCanvasGO.GetComponent<MainCanvas>();
    }

    #region Window Layer
    
    /// <summary>
    /// ��һ��UIWindow��
    /// </summary>
    /// <typeparam name="T">Ҫ�򿪵�UIWindow������</typeparam>
    /// <returns>����ʾ���ڵ�GameObject</returns>
    public GameObject ShowWindow<T>() where T : UIWindow
    {
        string name = typeof(T).Name;
        return ShowWindow(name);
    }

    /// <summary>
    /// ��һ��UIWindow��
    /// </summary>
    /// <param name="name">Ҫ�򿪵�UIWindow��������</param>
    /// <returns>����ʾ���ڵ�GameObject</returns>
    public GameObject ShowWindow(string name)
    {
        if (cachedWindows.ContainsKey(name))
        {
            cachedWindows[name].SetActive(true);
            return cachedWindows[name];
        }

        GameObject window = uiSettings.GetUIWindow(name);
        return Internal_ShowWindow(window, name);
    }
    
    /// <summary>
    /// �򿪶��ͬ���ʹ��ڡ�
    /// ��Ϊ����ͬ�ര�ڵĴ��ڣ��򿪺���ֱ��ͨ������/���ƹرգ�����ر��淵�ص�Unique name��ά���������ڡ�
    /// </summary>
    /// <typeparam name="T">Ҫ�򿪵�UIWindow������</typeparam>
    /// <returns>����ʾ���ڵ�GameObject��Unique name</returns>
    public Tuple<GameObject, string> ShowWindowMultiple<T>() where T : UIWindow
    {
        GameObject window = uiSettings.GetUIWindow<T>();
        string name = typeof(T).Name + DevUtils.SimpleGuid();
        return new Tuple<GameObject, string>(Internal_ShowWindow(window, name), name);
    }

    /// <summary>
    /// �򿪶��ͬ���ʹ��ڡ�
    /// ��Ϊ����ͬ�ര�ڵĴ��ڣ��򿪺���ֱ��ͨ������/���ƹرգ�����ر��淵�ص�Unique name��ά���������ڡ�
    /// </summary>
    /// <param name="name">Ҫ�򿪵�UIWindow��������</param>
    /// <returns>����ʾ���ڵ�GameObject��Unique name</returns>
    public Tuple<GameObject, string> ShowWindowMultiple(string name)
    {
        GameObject window = uiSettings.GetUIWindow(name);
        name += DevUtils.SimpleGuid();
        return new Tuple<GameObject, string>(Internal_ShowWindow(window, name), name);
    }

    /// <summary>
    /// ��һ��UIWindow��
    /// </summary>
    /// <returns>����ʾ���ڵ�GameObject</returns>
    private GameObject Internal_ShowWindow(GameObject window, string name)
    {
        if (window == null) return null;
        window = Instantiate(window);
        window.SetParent(mainCanvas.WindowLayer, false);
        window.transform.SetAsLastSibling();
        window.name = name;
        cachedWindows.Add(name, window);
        return window;
    }

    /// <summary>
    /// ����һ��UIWindow��
    /// </summary>
    /// <typeparam name="T">Ҫ���ص�UIWindow������</typeparam>
    public void HideWindow<T>() where T : UIWindow
    {
        string name = typeof(T).Name;
        HideWindow(name);
    }

    /// <summary>
    /// ����һ��UIWindow��
    /// </summary>
    /// <param name="name">Ҫ���ص�UIWindow��Unique Name</param>
    public void HideWindow(string name)
    {
        if (cachedWindows.ContainsKey(name))
        {
            cachedWindows[name].SetActive(false);
        }
        else
        {
            DevUtils.Log("Target window not showing");
        }
    }

    /// <summary>
    /// �ݻ�һ��UIWindow��
    /// ��ӻ�����ɾ�����ٴ���ʾ���������ɡ�
    /// </summary>
    /// <typeparam name="T">Ҫ�ݻٵ�UIWindow������</typeparam>
    public void DestroyWindow<T>() where T : UIWindow
    {
        string name = typeof(T).Name;
        DestroyWindow(name);
    }

    /// <summary>
    /// �ݻ�һ��UIWindow��
    /// ��ӻ�����ɾ�����ٴ���ʾ���������ɡ�
    /// </summary>
    /// <param name="name">Ҫ�ݻٵ�UIWindow��Unique Name</param>
    public void DestroyWindow(string name)
    {
        if (cachedWindows.ContainsKey(name))
        {
            GameObject window = cachedWindows[name];
            cachedWindows.Remove(name);
            Destroy(window);
        }
        else
        {
            DevUtils.Log("Target window not exist");
        }
    }

    #endregion

    #region Overlay Layer

    // TODO

    #endregion
    
    [Obsolete]
    public GameObject ShowUI(GameObject go) {
        string name = go.name;
        if (cachedUIs.ContainsKey(name))
        {
            cachedUIs[name].SetActive(true);
            return cachedUIs[name];
        }
        else { 
            go = Instantiate(go);
            go.transform.SetParent(contentParent, false);
            go.transform.SetAsLastSibling();
            cachedUIs.Add(name, go);
            return go;
        }
    }
    
    [Obsolete]
    public void HideUI(GameObject go) {
        string name = go.name;
        if (cachedUIs.ContainsKey(name))
        {
            cachedUIs[name].SetActive(false);
        }
        else {
            DevUtils.Log("Target UI not showing");
        }
    }
}
