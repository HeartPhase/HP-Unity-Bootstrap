using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Color类扩展
/// </summary>
public static class ColorExtension
{
    public static void SetAlpha(this Material m, float alpha) {
        Color temp = m.color;
        temp.a = alpha;
        m.color = temp;
    }

    public static void SetAlpha(this Image i, float alpha) {
        Color temp = i.color;
        temp.a = alpha;
        i.color = temp;
    }
}
