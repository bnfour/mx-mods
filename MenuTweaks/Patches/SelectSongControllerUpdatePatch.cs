using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.MenuTweaks.Patches;

/// <summary>
/// A patch that fixes the likes of 21st, 32nd, 43rd ranks all using th suffix
/// for the "small"/"list" menu (the one with a list visible on the right side);
/// 11th, 12th and 13th ranks keep the correct suffix.
/// </summary>
[HarmonyPatch(typeof(SelectSongController), "Update")]
public class SelectSongControllerUpdatePatch
{
    internal static void Postfix(SelectSongController __instance)
    {
        if (!Melon<MenuTweaksMod>.Instance.OrdinalFixEnabled)
        {
            return;
        }

        // this patch is really straightforward because all fields are accessible
        string pos = __instance.SmallRankNum.text;
        if (pos != "-")
        {
            if (pos.EndsWith("11") || pos.EndsWith("12") || pos.EndsWith("13"))
            {
                __instance.SmallThText.text = "th";
            }
            else if (pos.EndsWith("1"))
            {
                __instance.SmallThText.text = "st";
            }
            else if (pos.EndsWith("2"))
            {
                __instance.SmallThText.text = "nd";
            }
            else if (pos.EndsWith("3"))
            {
                __instance.SmallThText.text = "rd";
            }
            else
            {
                __instance.SmallThText.text = "th";
            }
        }
    }
}
