using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.MenuTweaks.Patches;

/// <summary>
/// A patch that fixes *11, *12, *13 ranks using st, nd, and rd suffix respectively
/// in the "big" song selection menu (the one with a big framed album art).
/// </summary>
[HarmonyPatch(typeof(SongInfoPackageScript), "Update")]
public class SongInfoPackageScriptUpdatePatch
{
    private const int SuffixTextIndex = 14;
    private const int SuffixShadowTextIndex = 25;
    private const int RankTextIndex = 13;

    internal static void Postfix(SongInfoPackageScript __instance)
    {
        if (!Melon<MenuTweaksMod>.Instance.OrdinalFixEnabled)
        {
            return;
        }

        var rank = __instance.newinfoText[RankTextIndex].text;
        if (rank.EndsWith("11") || rank.EndsWith("12") || rank.EndsWith("13"))
        {
            __instance.newinfoText[SuffixTextIndex].text = "TH";
            __instance.newinfoText[SuffixShadowTextIndex].text = "TH";
        }
    }
}
