using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// 记录并Serialize所有UIWindow子类到Prefab Asset的绑定。
/// </summary>
[System.Serializable]
public class UIWindowBindings
{
    public List<UIWindowBinding> bindings;

    public UIWindowBindings() { 
        bindings = new List<UIWindowBinding>();
    }

    /// <summary>
    /// 通过类型名获取所绑定的Prefab。
    /// </summary>
    public GameObject GetPrefab(string name) {
        if (bindings.Exists(b => b.windowName == name))
        {
            string prefabPath = bindings.Find(b => b.windowName == name).windowPrefabPath;
            return Resources.Load<GameObject>(prefabPath);
        }
        else {
            DevUtils.Log($"Window {name} not found in settings", "UISettings");
            return null;
        }
    }

    /// <summary>
    /// 添加一条绑定到UI Settings里
    /// </summary>
    public void AddBinding(string name, string prefabPath) {
        if (prefabPath == null) {
            DevUtils.Log($"Binding of {name} failed to find prefab", "UI Settings");
            return;
        }
        if (!bindings.Exists(b => b.windowName == name))
        {
            bindings.Add(new UIWindowBinding(name, prefabPath));
        }
        else {
            DevUtils.Log($"Duplicated binding of {name}", "UI Settings");
        }
    } 

    /// <summary>
    /// 用于记录UIWindow子类到对应Prefab资源路径的绑定。
    /// </summary>
    [System.Serializable]
    public class UIWindowBinding
    {
        public string windowName;
        public string windowPrefabPath;

        public UIWindowBinding(string _windowName, string _windowPrefabPath)
        {
            windowName = _windowName;
            windowPrefabPath = _windowPrefabPath;
        }
    }
}
