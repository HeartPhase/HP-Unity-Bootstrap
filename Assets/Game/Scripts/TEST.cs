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

    private void Awake()
    {
        //SaveModule.SwitchSaveSlot(SaveModule.GetNextValidSlotNumber());
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
        SaveModule.SaveData(GenTestData(),GenTestData());
    }

    public void TEST2_click()
    {
        SaveModule.WriteSlotSaveFile();
    }

    private string GenTestData()
    {
        return Random.Range(10000000, 99999999).ToString();
    }
    private void OnDisable()
    {

    }
}
