using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI模块。控制场景中窗口、界面等的显示。
/// </summary>
public class UIModule : MonoBehaviour, IGameModule
{
    public static GameObject mainCanvasGO;
    public static Canvas mainCanvas;
    private static Transform contentParent;

    private static Dictionary<string, GameObject> cachedUIs;

    /// <summary>
    /// 注册模块，创建主Canvas和UI缓存。
    /// </summary>
    public static void Init() {
        ModuleDispatcher.Instance.RegisterMono<UIModule>();
        DevUtils.Log("Inited", "UIModule");
        CreateMainCanvas();
        DevUtils.Log("Main canvas created", "UIModule");
        cachedUIs = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// 创建主Canvas。
    /// 这个路径是写死的，因为不需要改动。
    /// </summary>
    public static void CreateMainCanvas() {
        mainCanvasGO = Instantiate(Resources.Load<GameObject>("Components/MainCanvas/MainCanvas"));
        DontDestroyOnLoad(mainCanvasGO);
        mainCanvas = mainCanvasGO.GetComponent<Canvas>();
        contentParent = mainCanvasGO.transform.GetChild(0);
    }

    /// <summary>
    /// 显示指定的UI。
    /// 给SceneModule用的临时方法，后面主要开发这个模块的时候会改。
    /// </summary>
    /// <param name="go">需要显示的UI的Prefab</param>
    /// <returns>显示在main canvas里的实例化完成的此UI</returns>
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

    /// <summary>
    /// 隐藏指定的UI。
    /// </summary>
    /// <param name="go">需要隐藏的UI的Prefab</param>
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
