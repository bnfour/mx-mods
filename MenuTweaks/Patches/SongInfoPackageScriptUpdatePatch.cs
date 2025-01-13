using HarmonyLib;
using MelonLoader;
using UnityEngine.UI;

namespace Bnfour.MusynxMods.MenuTweaks.Patches;

/// <summary>
/// A patch that fixes *11, *12, *13 ranks using st, nd, and rd suffix respectively
/// in the "big" song selection menu (the one with a big framed album art).
/// </summary>
[HarmonyPatch(typeof(SongInfoPackageScript), "Update")]
public class SongInfoPackageScriptUpdatePatch
{
    internal static void Postfix(SongInfoPackageScript __instance)
    {
        if (!Melon<MenuTweaksMod>.Instance.OrdinalFixEnabled)
        {
            return;
        }

        // the field is internal and is not accessible from this patch;
        // might as well find the value in the newinfoText somewhere,
        // but it's not like using traverse tanks performance significantly
        var rankText = Traverse.Create(typeof(SteamLeaderBoardScript)).Property("miniLeaderBoardRank3").GetValue<Text>().text;
        // only applies the change if original method fails
        if (rankText.EndsWith("11") || rankText.EndsWith("12") || rankText.EndsWith("13"))
        {
            // one of these acts as a shadow to another, don't know/care which is which
            // indices taken straight from original method
            __instance.newinfoText[14].text = "TH";
            __instance.newinfoText[25].text = "TH";
        }
    }
}
