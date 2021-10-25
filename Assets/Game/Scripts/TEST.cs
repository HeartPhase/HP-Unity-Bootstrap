using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public void Clicked() {
        EditorUtils.CreateFromPrefab(Internal_PrefabEnum.Ball, EditorUtils.NewPrefabMode.BrandNew);
    }

    [ContextMenu("HPFramework / CreateCube")]
    public void AddCubeTest() {
        EditorUtils.CreateFromPrefab(Internal_PrefabEnum.Cube, EditorUtils.NewPrefabMode.OriginalAsBase);
    }
}
