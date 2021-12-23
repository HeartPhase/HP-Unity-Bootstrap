using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// ��¼��Serialize����UIWindow���ൽPrefab Asset�İ󶨡�
/// </summary>
[System.Serializable]
public class UIWindowBindings
{
    public List<UIWindowBinding> bindings;

    public UIWindowBindings() { 
        bindings = new List<UIWindowBinding>();
    }

    /// <summary>
    /// ͨ����������ȡ���󶨵�Prefab��
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
    /// ���һ���󶨵�UI Settings��
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
    /// ���ڼ�¼UIWindow���ൽ��ӦPrefab��Դ·���İ󶨡�
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
