using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// �������õ���һЩȫ�ֱ�����
/// </summary>
public static class FrameworkGlobals
{
    /// <summary>
    /// �����ĳЩ�ط�ʹ���˷ǳ������ġ�����д���롱�����Ǹ������ɵĴ����ļ���ͷ�ľ��档
    /// </summary>
    public static string GENERATED_SCRIPTS_WARNING = "///This is an auto-generated script, DO NOT modify OR delete it manually!\n///�����Զ����ɵĽű��������ֶ��޸Ļ�ɾ����\n";

    /// <summary>
    /// �����ַ���ʱʹ�õ�key�������Ҫ�浵���ܵ�������޸����key���Ҳ�Ҫ������
    /// </summary>
    public static byte[] ENCRYPTION_KEY = { 0x8f, 0xe7, 0x81, 0x8f, 0x23, 0x10, 0x44, 0xc3, 0xa0, 0x2c, 0x26, 0xb6, 0xbc, 0xfb, 0x23, 0x9 };
}
