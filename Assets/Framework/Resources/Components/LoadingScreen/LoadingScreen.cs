using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 最基础的加载画面。
/// </summary>
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] protected Scrollbar progressBar;
    [SerializeField] GameObject continueText;

    protected virtual void OnEnable()
    {
        progressBar.size = 0.0f;
        continueText.SetActive(false);
    }

    /// <summary>
    /// 设置进度条（范围0-1）。
    /// 重载此方法来自定义加载进度的呈现方式。
    /// </summary>
    public virtual void SetProgress(float progress) { 
        progressBar.size = progress;
    }

    /// <summary>
    /// 加载完成后显示“按任意键继续”。
    /// 重载此方法来自定义加载完成的确认方式。
    /// </summary>
    public virtual void ToggleComplete() { 
        continueText.SetActive(true);
    }
}
