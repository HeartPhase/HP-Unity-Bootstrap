using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// 模块监视器。
/// ModuleDispatcher初始化时搭载在Module Center，用于在运行时检查已注册的模块及Reference列表。
/// </summary>
public class ModuleDispatcherInspector : MonoBehaviour
{
    [TableList]
    [SerializeField, ReadOnly] List<ModuleDispatcherInspectorData> activeModules = new();

    [Button("RefreshData")]
    void RefreshData()
    {
        activeModules.Clear();
         Dictionary<string, IGameModule> modulesData = ModuleDispatcher.Instance.ReadModulesData();
        foreach (var module in modulesData) {
            if (module.Value is ScriptableObject) {
                activeModules.Add(new ModuleDispatcherInspectorData(
                    module.Key,
                    "Config Module",
                    (ScriptableObject)module.Value
                    ));
                continue;
            }
            bool isMono = module.Value is MonoBehaviour;
            activeModules.Add(new ModuleDispatcherInspectorData(
                module.Key,
                (isMono) ? "Mono" : "Script",
                (isMono) ? ((MonoBehaviour)module.Value).gameObject : null
                ));
        }
    }

    [System.Serializable]
    private class ModuleDispatcherInspectorData {
        public string name;
        public string type;
        public Object attachment;

        public ModuleDispatcherInspectorData(string _name, string _type, Object _attachment) { 
            name = _name;
            type = _type;
            attachment = _attachment;
        }
    }
}
