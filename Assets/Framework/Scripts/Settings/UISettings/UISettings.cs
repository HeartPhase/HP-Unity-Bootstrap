using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI Module�������ļ���
/// </summary>
[CreateAssetMenu(menuName = "Framework Settings/UI Settings")]
public class UISettings : ScriptableObject
{
    [SerializeField]
    UIWindowBindings bindings = new UIWindowBindings();

    /// <summary>
    /// ���һ��UI Window��
    /// </summary>
    public void AddWindowBinding(string name, string prefabPath) { 
        bindings.AddBinding(name, prefabPath);
    }

    /// <summary>
    /// ͨ��UI Window����������ȡ���󶨵�Prefab��
    /// </summary>
    public GameObject GetUIWindow(string name) {
        return bindings.GetPrefab(name);
    }


    /// <summary>
    /// ֱ��ͨ��UI Window�������ȡ���󶨵�Prefab��
    /// </summary>
    public GameObject GetUIWindow<T>() where T : UIWindow{ 
        string name = typeof(T).Name;
        return GetUIWindow(name);
    }
}
