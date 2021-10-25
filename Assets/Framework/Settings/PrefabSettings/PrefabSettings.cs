using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CreateAssetMenu(menuName ="Framework Settings/Prefab Settings")]
public class PrefabSettings : ScriptableObject
{
    //static string MODULE_NAME = "Standardised Prefab Collector";

    [SerializeField] string StandardisedPrefab_Path = "Assets/Framework/StandardisedPrefabs";

    private List<string> prefabPaths = new List<string>();

    [SerializeField] string ENUM_FILE = "Assets/Framework/Settings/PrefabSettings/Internal_PrefabEnum.cs";

    // todo: change this to a button, I don't have odin at work env.
    //private void OnValidate()
    //{
    //    if (string.IsNullOrEmpty(StandardisedPrefab_Path)) return;
    //    prefabPaths.Clear();
    //    ReadPrefabs();
    //    WritePrefabsEnumFile();
    //}

    private void ReadPrefabs() {
        string[] guids = AssetDatabase.FindAssets("t:prefab", new[] {StandardisedPrefab_Path});
        foreach (string guid in guids)
        {
            prefabPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
        }
    }

    private void WritePrefabsEnumFile() {
        string enumfile = "";
        string enumPrefix = "using System.ComponentModel; \npublic enum Internal_PrefabEnum { \n";
        string enumSuffix = "}\n";

        enumfile += enumPrefix;

        foreach (string path in prefabPaths) {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            DevUtils.Log(path);
            DevUtils.Log(prefab.name);
            enumfile += string.Format("    [Description(\"{0}\")]\n", path);
            enumfile += string.Format("    {0},\n", prefab.name);
        }
        enumfile += enumSuffix;
        File.WriteAllText(ENUM_FILE, enumfile);
    }
}
