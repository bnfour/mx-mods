using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Disables the custom text animation when the vanilla score text animation is
/// disabled.
/// </summary>
[HarmonyPatch(typeof(NewSettlementController), nameof(NewSettlementController.FalseBool))]
public class NewSettlementControllerFalseBoolPatch_StopTextAnimation
{
    internal static void Postfix(int num)
    {
        if (num == 4)
        {
            Melon<PlentifulStatsMod>.Instance.AnimationTick = null;
        }
    }
}
