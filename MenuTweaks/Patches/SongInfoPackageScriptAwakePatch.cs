using System.Linq;
using HarmonyLib;
using MelonLoader;
using UnityEngine.UI;

using Bnfour.MusynxMods.MenuTweaks.Utilities;

namespace Bnfour.MusynxMods.MenuTweaks.Patches;

[HarmonyPatch(typeof(SongInfoPackageScript), "Awake")]
public class SongInfoPackageScriptAwakePatch
{
    internal static void Postfix(SongInfoPackageScript __instance)
    {
        if (!Melon<MenuTweaksMod>.Instance.ShadowNormalizationEnabled
            // should never be null if the feature is enabled, but let's check anyway
            || Melon<MenuTweaksMod>.Instance.shadowColorHelper is not ShadowColorHelper helper)
        {
            return;
        }

        // GetComponentsInChildren<Text> gets everything but the composer info;
        // newinfoText gets everything but the "world" and "ranking";
        // some texts are included twice, but most are not changed anyway
        foreach (var text in __instance.GetComponentsInChildren<Text>(true).Concat(__instance.newinfoText))
        {
            helper.Process(text);
        }
    }
}
