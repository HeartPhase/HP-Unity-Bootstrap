using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIģ�顣���Ƴ����д��ڡ�����ȵ���ʾ��
/// </summary>
public class UIModule : MonoBehaviour, IGameModule
{
    public static GameObject mainCanvasGO;
    public static Canvas mainCanvas;
    private static Transform contentParent;

    private static Dictionary<string, GameObject> cachedUIs;

    /// <summary>
    /// ע��ģ�飬������Canvas��UI���档
    /// </summary>
    public static void Init() {
        ModuleDispatcher.Instance.RegisterMono<UIModule>();
        DevUtils.Log("Inited", "UIModule");
        CreateMainCanvas();
        DevUtils.Log("Main canvas created", "UIModule");
        cachedUIs = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// ������Canvas��
    /// ���·����д���ģ���Ϊ����Ҫ�Ķ���
    /// </summary>
    public static void CreateMainCanvas() {
        mainCanvasGO = Instantiate(Resources.Load<GameObject>("Components/MainCanvas/MainCanvas"));
        DontDestroyOnLoad(mainCanvasGO);
        mainCanvas = mainCanvasGO.GetComponent<Canvas>();
        contentParent = mainCanvasGO.transform.GetChild(0);
    }

    /// <summary>
    /// ��ʾָ����UI��
    /// ��SceneModule�õ���ʱ������������Ҫ�������ģ���ʱ���ġ�
    /// </summary>
    /// <param name="go">��Ҫ��ʾ��UI��Prefab</param>
    /// <returns>��ʾ��main canvas���ʵ������ɵĴ�UI</returns>
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
    /// ����ָ����UI��
    /// </summary>
    /// <param name="go">��Ҫ���ص�UI��Prefab</param>
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
