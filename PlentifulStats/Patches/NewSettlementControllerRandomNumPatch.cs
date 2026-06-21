using System.Linq;

using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Ticks up the best value along with the vanilla numbers changing randomly,
/// if enabled to do so.
/// </summary>
[HarmonyPatch(typeof(NewSettlementController), "RandomNum")]
public class NewSettlementControllerRandomNumPatch
{
    // found empirically
    // one tick is 1/20 s, checks out with the ~2.7 s value used previously
    // (this starts to run a bit later)
    private const int MaximumTick = 52;

    internal static void Postfix(NewSettlementController __instance)
    {
        var modInstance = Melon<PlentifulStatsMod>.Instance;
        // AnimationTick being null implies either the feature is not enabled
        // and it was never set to a value,
        // or we're already done, and it's reset so we do nothing while vanilla
        // code continues to call RandomNum until the stats screen is dismissed
        if (modInstance.AnimationTick.HasValue)
        {
            modInstance.AnimationTick++;
            var t = Mathf.Min(1, (float)modInstance.AnimationTick / MaximumTick);

            __instance.UIText.FirstOrDefault(tmp => tmp.name == "BnPrevBestValue")?.text =
                Mathf.SmoothStep(0, (float)modInstance.SyncNumber / 100, t)
                .ToString("0.00") + "%";
        }
    }
}
