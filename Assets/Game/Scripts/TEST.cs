using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEST : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;

    [SerializeField] TMPro.TMP_Text devText;

    private InputModule m_input;
    private UIModule m_ui;

    private List<string> uniqueNames;
    private void Awake()
    {
        InputModule.Gameplay_Interact += LogTestMsg;
        m_input = ModuleDispatcher.Instance.Get<InputModule>();
        m_ui = ModuleDispatcher.Instance.Get<UIModule>();
        uniqueNames = new();
    }

    public void TEST1_click()
    {
        //bool showingDuplicateInfo = false;
        //string actionName = "Gameplay/Interact";
        //devText.text = $"Rebinding - {actionName}";
        //m_input.RemapAction("Gameplay/Interact",
        //    () => { if (!showingDuplicateInfo) devText.text = "Complete"; },
        //    () => { if (!showingDuplicateInfo) devText.text = "Cancelled"; },
        //    (duplicated, path) => { devText.text = $"{path} is taken for another action!"; showingDuplicateInfo = true; }
        //);
        var res = m_ui.ShowWindowMultiple<TestWindow>();
        uniqueNames.Add(res.Item2);
    }

    public void TEST2_click()
    {
        if (uniqueNames.Count >= 0)
        {
            string randomName = uniqueNames[0];
            uniqueNames.Remove(randomName);
            m_ui.HideWindow(randomName);
        }
        
        //devText.text = "Interact: " + m_input.GetCurrentBinding("Gameplay/Interact");
    }

    private void LogTestMsg() {
        DevUtils.Log("Interacted", "Test");
    }
    private void OnDisable()
    {
        InputModule.Gameplay_Interact -= LogTestMsg;
    }
}
