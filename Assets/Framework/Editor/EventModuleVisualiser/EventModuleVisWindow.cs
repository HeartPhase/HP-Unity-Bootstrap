using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using UnityEditor;

/// <summary>
/// �¼�ģ���Debug���ߡ�
/// ������ʱʵʱ��ʾ��ע����¼����������¼����༰������
/// </summary>
public class EventModuleVisWindow : OdinEditorWindow
{
    [MenuItem("Framework/Visualisers/EventModule-Events")]
    private static void OpenWindow() { 
        GetWindow<EventModuleVisWindow>().Show();
    }

    [Button("Update Data")]
    void UpdateData()
    {
        if (Application.isPlaying)
        {
            visualData = new List<EventInspectorData>();
            EventModule module = ModuleDispatcher.Instance.Get<EventModule>();
            Dictionary<string, Action<EventArgs>> data = module.ReadEventsData();
            foreach (KeyValuePair<string, Action<EventArgs>> entry in data) {
                visualData.Add(new EventInspectorData(entry.Key, entry.Value.GetInvocationList()));
            }
        }
    }
    [SerializeField, ReadOnly]
    private List<EventInspectorData> visualData;
}

[Serializable]
public class EventInspectorData {
    public string name;
    public List<string> actions = new List<string>();
    public EventInspectorData(string name, Delegate[] actions)
    {
        this.name = name;
        foreach (Delegate action in actions)
        {
            this.actions.Add($"{action.Method.Name}@{action.Method.DeclaringType}");
        }
    }
}
