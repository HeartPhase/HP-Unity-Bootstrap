using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��һ��Monoģ������ã�û�й��ܡ�
/// </summary>
public class NonSenseModule : MonoBehaviour, IGameModule
{
    public void Init()
    {
        DevUtils.Log("Inited", "NonSenseModule");
    }
}
