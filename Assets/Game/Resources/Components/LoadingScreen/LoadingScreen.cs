using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Scrollbar progressbar;

    public void SetProgress(float progress) { 
        progressbar.size = progress;
    }
}
