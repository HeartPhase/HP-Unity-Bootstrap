using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI模块。控制场景中窗口、界面等的显示。
/// </summary>
public class UIModule : MonoBehaviour, IGameModule
{
    /// <summary>
    /// 注册模块。
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
    /// 创建主Canvas和UI缓存,读取UI模块配置。
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
    /// 创建主Canvas。
    /// 这个路径是写死的，因为不需要改动。
    /// </summary>
    public static void CreateMainCanvas() {
        mainCanvasGO = Instantiate(Resources.Load<GameObject>("Components/MainCanvas/MainCanvas"));
        mainCanvasGO.name = "Main Canvas";
        DontDestroyOnLoad(mainCanvasGO);
        mainCanvas = mainCanvasGO.GetComponent<MainCanvas>();
    }

    #region Window Layer
    
    /// <summary>
    /// 打开一个UIWindow。
    /// </summary>
    /// <typeparam name="T">要打开的UIWindow的类型</typeparam>
    /// <returns>所显示窗口的GameObject</returns>
    public GameObject ShowWindow<T>() where T : UIWindow
    {
        string name = typeof(T).Name;
        return ShowWindow(name);
    }

    /// <summary>
    /// 打开一个UIWindow。
    /// </summary>
    /// <param name="name">要打开的UIWindow的类型名</param>
    /// <returns>所显示窗口的GameObject</returns>
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
    /// 打开多个同类型窗口。
    /// 因为复数同类窗口的存在，打开后不能直接通过类型/名称关闭，请务必保存返回的Unique name来维护生命周期。
    /// </summary>
    /// <typeparam name="T">要打开的UIWindow的类型</typeparam>
    /// <returns>所显示窗口的GameObject和Unique name</returns>
    public Tuple<GameObject, string> ShowWindowMultiple<T>() where T : UIWindow
    {
        GameObject window = uiSettings.GetUIWindow<T>();
        string name = typeof(T).Name + DevUtils.SimpleGuid();
        return new Tuple<GameObject, string>(Internal_ShowWindow(window, name), name);
    }

    /// <summary>
    /// 打开多个同类型窗口。
    /// 因为复数同类窗口的存在，打开后不能直接通过类型/名称关闭，请务必保存返回的Unique name来维护生命周期。
    /// </summary>
    /// <param name="name">要打开的UIWindow的类型名</param>
    /// <returns>所显示窗口的GameObject和Unique name</returns>
    public Tuple<GameObject, string> ShowWindowMultiple(string name)
    {
        GameObject window = uiSettings.GetUIWindow(name);
        name += DevUtils.SimpleGuid();
        return new Tuple<GameObject, string>(Internal_ShowWindow(window, name), name);
    }

    /// <summary>
    /// 打开一个UIWindow。
    /// </summary>
    /// <returns>所显示窗口的GameObject</returns>
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
    /// 隐藏一个UIWindow。
    /// </summary>
    /// <typeparam name="T">要隐藏的UIWindow的类型</typeparam>
    public void HideWindow<T>() where T : UIWindow
    {
        string name = typeof(T).Name;
        HideWindow(name);
    }

    /// <summary>
    /// 隐藏一个UIWindow。
    /// </summary>
    /// <param name="name">要隐藏的UIWindow的Unique Name</param>
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
    /// 摧毁一个UIWindow。
    /// 会从缓存中删除，再次显示会重新生成。
    /// </summary>
    /// <typeparam name="T">要摧毁的UIWindow的类型</typeparam>
    public void DestroyWindow<T>() where T : UIWindow
    {
        string name = typeof(T).Name;
        DestroyWindow(name);
    }

    /// <summary>
    /// 摧毁一个UIWindow。
    /// 会从缓存中删除，再次显示会重新生成。
    /// </summary>
    /// <param name="name">要摧毁的UIWindow的Unique Name</param>
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
