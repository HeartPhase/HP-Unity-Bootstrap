using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ļ��ػ��档
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
    /// ���ý���������Χ0-1����
    /// ���ش˷������Զ�����ؽ��ȵĳ��ַ�ʽ��
    /// </summary>
    public virtual void SetProgress(float progress) { 
        progressBar.size = progress;
    }

    /// <summary>
    /// ������ɺ���ʾ�����������������
    /// ���ش˷������Զ��������ɵ�ȷ�Ϸ�ʽ��
    /// </summary>
    public virtual void ToggleComplete() { 
        continueText.SetActive(true);
    }
}
