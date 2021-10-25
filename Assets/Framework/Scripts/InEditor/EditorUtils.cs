using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class EditorUtils
{
    static string PREFAB_TARGET_PATH = "Assets/Game/Resources/Prefabs/";
    public static void CreateFromPrefab(Internal_PrefabEnum prefab) {
        string prefabPath = prefab.GetDescription();
        GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        var inited = (GameObject)PrefabUtility.InstantiatePrefab(go);

        if (!Directory.Exists(PREFAB_TARGET_PATH + go.name)) {
            Directory.CreateDirectory(PREFAB_TARGET_PATH + go.name);
        }
        int index = 0;
        while (File.Exists(ConstructPrefabPath(go.name, index)))
        {
            index++;
        }
        PrefabUtility.SaveAsPrefabAsset(inited, ConstructPrefabPath(go.name, index), out bool success);
        DevUtils.Log(success);
        Object.Destroy(inited);
    }

    private static string ConstructPrefabPath(string name, int index) => PREFAB_TARGET_PATH + name + "/" + name + "_" + index.ToString() + ".prefab";
}
