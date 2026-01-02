using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.SongInfo.Patches;

/// <summary>
/// Patches the "big" menu UI to include text fields to display the custom data.
/// </summary>
[HarmonyPatch(typeof(SongInfoPackageScript), "Awake")]
public class SongInfoPackageScriptAwakePatch
{
    private const int OriginalShadowIndex = 25;
    private const int OriginalIndex = 14;
    private const int SongTitleIndex = 0;
    private const int SongTitleShadowIndex = 1;

    // TODO tweak position a bit?
    private static readonly Vector3 _basePosition = new(-480, -155, 0);
    private static readonly Vector2 _size = new(400, 40);

    internal static void Postfix(SongInfoPackageScript __instance)
    {
        // make sure only the prefab (that actual instances are cloned from) gets modified
        // if the modifications are already present, do nothing
        if (__instance.newinfoText[__instance.newinfoText.Length - 1].name.StartsWith("bnBigMenuSongInfo"))
        {
            return;
        }

        // offset between original texts that creates shadow effect similar to css text-shadow
        var shadowOffset = __instance.newinfoText[OriginalShadowIndex].rectTransform.anchoredPosition3D - __instance.newinfoText[OriginalIndex].rectTransform.anchoredPosition3D;

        // clone order matters for "z-ordering"
        // the parent is set to the title text's parent so the clones are not disabled with the results panel when there are no user record for a level
        var shadowClone = GameObject.Instantiate(__instance.newinfoText[OriginalShadowIndex], __instance.newinfoText[SongTitleIndex].transform.parent);
        shadowClone.name = "bnBigMenuSongInfoShadow";
        shadowClone.text = string.Empty;
        shadowClone.rectTransform.anchoredPosition3D = _basePosition + shadowOffset;
        shadowClone.rectTransform.sizeDelta = _size;
        shadowClone.alignment = TextAnchor.UpperLeft;
        // set the shadow color from the song title's shadow -- it differs from the ranking panel
        shadowClone.color = __instance.newinfoText[SongTitleShadowIndex].color;

        var textClone = GameObject.Instantiate(__instance.newinfoText[OriginalIndex], __instance.newinfoText[SongTitleIndex].transform.parent);
        textClone.name = "bnBigMenuSongInfo";
        textClone.text = string.Empty;
        textClone.rectTransform.anchoredPosition3D = _basePosition;
        textClone.rectTransform.sizeDelta = _size;
        textClone.alignment = TextAnchor.UpperLeft;

        // add to the end of an existing convenience(?) array for easy access
        // and fade effect support
        __instance.newinfoText = [.. __instance.newinfoText, shadowClone, textClone];
    }
}
