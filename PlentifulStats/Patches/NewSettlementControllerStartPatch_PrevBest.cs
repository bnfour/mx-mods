using System.Collections;

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
public class NewSettlementControllerStartPatch_PrevBest
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
        newScoreBg.transform.position = scoreBg.transform.position + new Vector3(0, -27, 0);


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
        extraHeader.transform.position = originalHeader.transform.position + new Vector3(3, -63, 0);
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
            ? ((float)prevBest / 100).ToString("0.00") + "%"
            : "--";
        extraValue.transform.position = originalHeader.transform.position + new Vector3(326, -36, 0);
        // required for it to disappear properly
        __instance.UIText = __instance.UIText.AddToArray(extraValue);

        // TODO this uses a separate timer and the changes are not synced to the rest of the changing UI;
        // it should be possible to patch into RandomNum method to get in time updates,
        // ...but is it really worth it?
        // two texts being updated 20 times a second with a slight desync is definitely noticeable
        // if you know it's there and really look for it
        if (Melon<PlentifulStatsMod>.Instance.AnimatePrevBest)
        {
            MelonCoroutines.Start(ScoreAnimationCoroutine(prevBest, extraValue));
        }
    }

    private static IEnumerator ScoreAnimationCoroutine(int prevBest, TMPro.TextMeshPro component)
    {
        // taken from the animation
        const float animationEndTime = 2.7333f;
        // clamp updates to 20 fps similar to vanilla random values
        const float timeBetweenUpdates = (float)1 / 20;

        float time = 0;
        float lastUpdated = -1;
        
        while ((time += Time.deltaTime) < animationEndTime)
        {
            if (time - lastUpdated >= timeBetweenUpdates)
            {
                component.text = Mathf.SmoothStep(0, (float)prevBest / 100, time / animationEndTime).ToString("0.00") + "%";
                lastUpdated = time;
            }
            yield return null;
        }
        component.text = ((float)prevBest / 100).ToString("0.00") + "%";
    }
}

