using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Patch to fix Next button for 120+ scores on EN locale.
/// </summary>
[HarmonyPatch(typeof(NewSettlementController), "Start")]
public class NewSettlementControllerStartPatch_EnNextFix
{
    internal static void Prefix(NewSettlementController __instance)
    {
        if (!Melon<PlentifulStatsMod>.Instance.EnNextFix)
        {
            return;
        }

        // by default, the CN sprite is used,
        // so other locales should be adjusted
        // (there's only two sprites, CN and EN, with EN used on non-CN locales
        //   on the other button renderer that works as intended)
        if (MultilingualText.gL() != "cn")
        {
            __instance.newNextRenderer.sprite = __instance.ENImage[0];
        }
    }
}
