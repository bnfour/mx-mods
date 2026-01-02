using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace Bnfour.MusynxMods.MenuTweaks.Patches;

[HarmonyPatch(typeof(SongInfoPackageScript), "Awake")]
public class XddPatch
{
    private static readonly Color LighterShadow = new(0.235f, 0.235f, 0.235f, 1.000f);

    internal static void Postfix(SongInfoPackageScript __instance)
    {
        // gets everything but the composer info
        var texts = __instance.GetComponentsInChildren<Text>(true);
        foreach (var text in texts)
        {
            if (CloseEnough(text.color, Color.black))
            {
                text.color = Color.cyan;
            }
            else if (CloseEnough(text.color, LighterShadow))
            {
                text.color = Color.magenta;
            }
        }
        // gets everything but the "world" and "ranking"
        foreach (var tmp in __instance.newinfoText)
        {
            if (CloseEnough(tmp.color, Color.black))
            {
                tmp.color = Color.green;
            }
            else if (CloseEnough(tmp.color, LighterShadow))
            {
                tmp.color = Color.red;
            }
        }
    }

    private static bool CloseEnough(Color one, Color two)
    {
        const float eps = 0.001f;
        return Math.Abs(one.r - two.r) <= eps
            && Math.Abs(one.g - two.g) <= eps
            && Math.Abs(one.b - two.b) <= eps
            && Math.Abs(one.a - two.a) <= eps;
    }
}
