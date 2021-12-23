using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 框架里会用到的一些全局变量。
/// </summary>
public static class FrameworkGlobals
{
    /// <summary>
    /// 框架在某些地方使用了非常暴力的“代码写代码”，这是附在生成的代码文件开头的警告。
    /// </summary>
    public static string GENERATED_SCRIPTS_WARNING = "///This is an auto-generated script, DO NOT modify OR delete it manually!\n///这是自动生成的脚本，请勿手动修改或删除！\n";

    /// <summary>
    /// 框架默认设置的位置，如果要修改或替换默认设置，这里也需要修改。
    /// </summary>
    public static FrameworkSettings DEFAULT_SETTINGS => Resources.Load("Settings/DefaultFrameworkSettings") as FrameworkSettings;

    /// 加密字符串时使用的key，如果需要存档加密等请务必修改这个key并且不要公开！
    /// </summary>
    public static byte[] ENCRYPTION_KEY = { 0x8f, 0xe7, 0x81, 0x8f, 0x23, 0x10, 0x44, 0xc3, 0xa0, 0x2c, 0x26, 0xb6, 0xbc, 0xfb, 0x23, 0x9 };
}
