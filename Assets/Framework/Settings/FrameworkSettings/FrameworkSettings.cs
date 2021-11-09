using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Framework Settings/Framework Settings")]
public class FrameworkSettings : ScriptableObject
{
    [SerializeField]
    public GameObject loadingScreen;
}
