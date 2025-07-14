using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.SongInfo.Patches;

/// <summary>
/// Patches the "small" menu UI to include a text field to display the custom data.
/// </summary>
[HarmonyPatch(nameof(SelectSongController), nameof(SelectSongController.SmallScene))]
public class SelectSongControllerSmallScenePatch
{
    // TODO tweak position a bit?
    // offset from the original component
    private static readonly Vector3 _offset = new(-475, 10, 0);

    internal static void Prefix(SelectSongController __instance)
    {
        // only add the custom textfield once per instance
        if (__instance.SongComposerInfoText[__instance.SongComposerInfoText.Length - 1].name.StartsWith("bnPostfixClone"))
        {
            return;
        }

        var clone = GameObject.Instantiate(__instance.SongComposerInfoText[0], __instance.SongComposerInfoText[0].transform.parent);
        clone.name = "bnPostfixClone0";
        clone.text = string.Empty;
        clone.rectTransform.anchoredPosition3D += _offset;

        __instance.SongComposerInfoText = [.. __instance.SongComposerInfoText, clone];
    }
}
