using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject¿‡¿©’π
/// </summary>
public static class GameObjectExtension
{
    public static void SetParent(this GameObject go, GameObject target, bool keepPosition = true) {
        go.transform.SetParent(target.transform, keepPosition);
    }

    public static GameObject GetParent(this GameObject go) {
        return go.transform.parent.gameObject;
    }
}
