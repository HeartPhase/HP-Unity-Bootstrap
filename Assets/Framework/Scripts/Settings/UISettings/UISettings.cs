using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI Module的配置文件。
/// </summary>
[CreateAssetMenu(menuName = "Framework Settings/UI Settings")]
public class UISettings : ScriptableObject
{
    [SerializeField]
    UIWindowBindings bindings = new UIWindowBindings();

    /// <summary>
    /// 添加一条UI Window绑定
    /// </summary>
    public void AddWindowBinding(string name, string prefabPath) { 
        bindings.AddBinding(name, prefabPath);
    }

    /// <summary>
    /// 通过UI Window的子类名获取所绑定的Prefab。
    /// </summary>
    public GameObject GetUIWindow(string name) {
        return bindings.GetPrefab(name);
    }


    /// <summary>
    /// 直接通过UI Window的子类获取所绑定的Prefab。
    /// </summary>
    public GameObject GetUIWindow<T>() where T : UIWindow{ 
        string name = typeof(T).Name;
        return GetUIWindow(name);
    }
}
