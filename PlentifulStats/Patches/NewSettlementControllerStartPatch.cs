using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Patch to modify the stats screen UI just before it's shown
/// to include layout for previous best.
/// </summary>
// a private method again
[HarmonyPatch(typeof(NewSettlementController), "Start")]
public class NewSettlementControllerStartPatch
{
    internal static void Prefix(NewSettlementController __instance)
    {
        if (!Melon<PlentifulStatsMod>.Instance.PrevBest)
        {
            return;
        }

        // dark background for the score display, we need to resize it,
        // but it has texts attached and these will be resized too T_T
        // so we make a clone, remove cloned texts and resize at will
        // while hiding the original background
        var scoreBg = __instance.toBackDimRenderers[2];
        var newScoreBg = UnityEngine.Object.Instantiate(scoreBg, scoreBg.transform.parent);
        newScoreBg.name = "BnScoreBg";
        // remove text components from the clone
        var texts = newScoreBg.GetComponentsInChildren<TMPro.TextMeshPro>();
        foreach (var component in texts)
        {
            component.transform.parent = null;
            UnityEngine.Object.Destroy(component);
        }
        // this is required for the background to be included in the disappearing animation
        __instance.toBackDimRenderers = __instance.toBackDimRenderers.AddToArray(newScoreBg);
        // hide the original sprite
        scoreBg.color = Color.clear;
        // resize&move the new background
        newScoreBg.transform.localScale = new Vector3
        {
            x = scoreBg.transform.localScale.x,
            y = 92,
            z = scoreBg.transform.localScale.z
        };
        newScoreBg.transform.position = new Vector3
        {
            x = scoreBg.transform.position.x,
            y = scoreBg.transform.position.y - 27,
            z = scoreBg.transform.position.z
        };


        // the "SYNC.RATE" text, we'll need a copy for the new header
        var originalHeader = __instance.UIText[0];
        var extraHeader = UnityEngine.Object.Instantiate(originalHeader, originalHeader.transform.parent);
        extraHeader.name = "BnPrevBestHeader";
        // TODO consider non-English locales if the other texts are different
        extraHeader.text = "SYNC.BEST";
        // it was not very fun moving all this stuff around,
        // so these offsets here and for other components
        // are the first values i got that received
        // "meh, serviceable enough" reaction from me
        extraHeader.transform.position = new Vector3
        {
            x = originalHeader.transform.position.x + 3,
            y = originalHeader.transform.position.y - 63,
            z = originalHeader.transform.position.z
        };
        // required for it to disappear properly
        __instance.UIText = __instance.UIText.AddToArray(extraHeader);


        // the percentage of the score to be added from exact hits,
        // a copy is used to display the previous high score
        var originalValue = __instance.UIText[6];
        // note the parent for the clone
        var extraValue = UnityEngine.Object.Instantiate(originalValue, originalHeader.transform.parent);
        extraValue.name = "BnPrevBestValue";

        var prevBest = Melon<PlentifulStatsMod>.Instance.SyncNumber;
        extraValue.text = prevBest > 0
            ? ((float)prevBest / 100).ToString("00.00") + "%"
            : "--";
        extraValue.transform.position = new Vector3
        {
            x = originalHeader.transform.position.x + 326,
            y = originalHeader.transform.position.y - 36,
            z = originalHeader.transform.position.z
        };
        // required for it to disappear properly
        __instance.UIText = __instance.UIText.AddToArray(extraValue);
    }
}

