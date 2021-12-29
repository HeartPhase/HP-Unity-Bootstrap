using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class TEST : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;

    private EventModule EM => ModuleDispatcher.Instance.Get<EventModule>();
    private void Awake()
    {
        //SaveModule.SwitchSaveSlot(SaveModule.GetNextValidSlotNumber());
        EM.Listen("Test Event", TestResponse);
        EM.Listen("Test Event", TestResponse2);
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
        //SaveModule.SaveData(GenTestData(),GenTestData());
        EM.Remove("Test Event", TestResponse);
        EM.Remove("Test Event", TestResponse2);
    }

    private void TestResponse(EventArgs obj)
    {
        DevUtils.Log("Hi there");
    }

    private void TestResponse2(EventArgs obj)
    {
        DevUtils.Log("Hi there too");
    }

    public void TEST2_click()
    {
        EM.Invoke("Test Event");
    }

    private string GenTestData()
    {
        return Random.Range(10000000, 99999999).ToString();
    }
    private void OnDisable()
    {

    }
}
