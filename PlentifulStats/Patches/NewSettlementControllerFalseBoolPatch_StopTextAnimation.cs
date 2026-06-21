using System.Linq;

using HarmonyLib;
using MelonLoader;

using Bnfour.MusynxMods.PlentifulStats.Data;
using Bnfour.MusynxMods.PlentifulStats.Utilities;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Disables the custom text animation when the vanilla score text animation is
/// disabled, sets the final value.
/// </summary>
[HarmonyPatch(typeof(NewSettlementController), nameof(NewSettlementController.FalseBool))]
public class NewSettlementControllerFalseBoolPatch_StopTextAnimation
{
    internal static void Postfix(NewSettlementController __instance, int num)
    {
        if (num == 4)
        {
            var mod = Melon<PlentifulStatsMod>.Instance;
            mod.AnimationTick = null;

            __instance.UIText.FirstOrDefault(tmp => tmp.name == Constants.CustomValueName)?.text
                = ScoreFormatter.FormatSyncNumber(mod.SyncNumber);
        }
    }
}
