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
}
