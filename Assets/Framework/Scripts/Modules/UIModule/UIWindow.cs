using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWindow : MonoBehaviour {
#if UNITY_EDITOR
    protected virtual void Reset()
    {
        string prefabPath = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage().assetPath;
        int cutPoint = prefabPath.IndexOf("Resources/");
        if (cutPoint == -1) {
            DevUtils.Log($"{prefabPath} is not under Resources/", "New UI Window");
            return;
        }
        string prefabResourcesPath = prefabPath.Substring(cutPoint + 10).Replace(".prefab","");
        FrameworkGlobals.DEFAULT_SETTINGS.uiSettings.AddWindowBinding(GetType().Name, prefabResourcesPath);
        DevUtils.Log($"{this.GetType().Name} is attached to prefab@{prefabResourcesPath}", "New UI Window");
    }
#endif
}

