using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.SongInfo.Patches;

/// <summary>
/// Patches the "big" menu UI to include text fields to display the custom data.
/// </summary>
[HarmonyPatch(nameof(SongInfoPackageScript), "Awake")]
public class SongInfoPackageScriptAwakePatch
{
    // TODO tweak position a bit?
    private static readonly Vector3 _basePosition = new(-480, -155, 0);
    private static readonly Vector2 _size = new(400, 40);

    internal static void Postfix(SongInfoPackageScript __instance)
    {
        // make sure only the prefab (that actual instances are cloned from) gets modified
        // if the modifications are already present, do nothing
        if (__instance.newinfoText[__instance.newinfoText.Length - 1].name.StartsWith("bnPostfixClone"))
        {
            return;
        }

        // offset between original texts that creates shadow effect similar to css text-shadow
        var shadowOffset = __instance.newinfoText[25].rectTransform.anchoredPosition3D - __instance.newinfoText[14].rectTransform.anchoredPosition3D;

        // clone order matters for the shadow
        // the parent is set to the title text's parent so the clones are not disabled with the results panel when there are no user record for a level
        var cloneOne = GameObject.Instantiate(__instance.newinfoText[25], __instance.newinfoText[0].transform.parent);
        cloneOne.name = "bnPostfixClone25";
        cloneOne.text = string.Empty;
        cloneOne.rectTransform.anchoredPosition3D = _basePosition + shadowOffset;
        cloneOne.rectTransform.sizeDelta = _size;
        cloneOne.alignment = TextAnchor.UpperLeft;

        var cloneTwo = GameObject.Instantiate(__instance.newinfoText[14], __instance.newinfoText[0].transform.parent);
        cloneTwo.name = "bnPostfixClone14";
        cloneTwo.text = string.Empty;
        cloneTwo.rectTransform.anchoredPosition3D = _basePosition;
        cloneTwo.rectTransform.sizeDelta = _size;
        cloneTwo.alignment = TextAnchor.UpperLeft;

        // add to the end of an existing convenience(?) array for easy access
        // and fade effect support
        __instance.newinfoText = [.. __instance.newinfoText, cloneOne, cloneTwo];
    }
}
