using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Patch that changes the number of blue + cyan exacts to blue exacts only
/// after the number-changing animation is done.
/// (If enabled via preferences)
/// </summary>
[HarmonyPatch(typeof(NewSettlementController), nameof(NewSettlementController.FalseBool))]
public class NewSettlementControllerFalseBoolPatch
{
    private static bool Prefix(int num, NewSettlementController __instance,
        // these are all private fields
        ref bool ___exactRandomBool, int ___exNumber, int ___exactNumber)
    {
        if (!Melon<PlentifulStatsMod>.Instance.SeparateExacts
            || num != 0)
        {
            return true;
        }
        // reset the flag and set the number of cyan exacts as in original method,
        // but assign different value to the number of blue exacts
        ___exactRandomBool = false;
        __instance.Result[0].text = ___exNumber.ToString();
        __instance.Result[1].text = (___exactNumber - ___exNumber).ToString();

        return false;
    }
}

