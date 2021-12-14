using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEST : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;

    [SerializeField] TMPro.TMP_Text devText;

    private InputModule m_input;
    private void Awake()
    {
        InputModule.Gameplay_Interact += LogTestMsg;
        m_input = ModuleDispatcher.Instance.Get<InputModule>();
    }

    public void TEST1_click()
    {
        bool showingDuplicateInfo = false;
        string actionName = "Gameplay/Interact";
        devText.text = $"Rebinding - {actionName}";
        m_input.RemapAction("Gameplay/Interact",
            () => { if (!showingDuplicateInfo) devText.text = "Complete"; },
            () => { if (!showingDuplicateInfo) devText.text = "Cancelled"; },
            (duplicated, path) => { devText.text = $"{path} is taken for another action!"; showingDuplicateInfo = true; }
        );
    }

    public void TEST2_click()
    {
        devText.text = "Interact: " + m_input.GetCurrentBinding("Gameplay/Interact");
    }

    private void LogTestMsg() {
        DevUtils.Log("Interacted", "Test");
    }
    private void OnDisable()
    {
        InputModule.Gameplay_Interact -= LogTestMsg;
    }
}
