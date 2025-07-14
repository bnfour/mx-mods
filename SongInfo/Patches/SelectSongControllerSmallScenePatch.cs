using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.SongInfo.Patches;

/// <summary>
/// Patches the "small" menu UI to include a text field to display the custom data.
/// </summary>
[HarmonyPatch(nameof(SelectSongController), nameof(SelectSongController.SmallScene))]
public class SelectSongControllerSmallScenePatch
{
    private const int OriginalTextIndex = 0;
    // TODO tweak position a bit?
    // offset from the original component
    private static readonly Vector3 _offset = new(-475, 10, 0);

    internal static void Prefix(SelectSongController __instance)
    {
        // only add the custom textfield once per instance
        if (__instance.SongComposerInfoText[__instance.SongComposerInfoText.Length - 1].name.Equals("bnSmallMenuSongInfo"))
        {
            return;
        }

        var clone = GameObject.Instantiate(__instance.SongComposerInfoText[OriginalTextIndex], __instance.SongComposerInfoText[OriginalTextIndex].transform.parent);
        clone.name = "bnSmallMenuSongInfo";
        clone.text = string.Empty;
        clone.rectTransform.anchoredPosition3D += _offset;
        // for easy access
        __instance.SongComposerInfoText = [.. __instance.SongComposerInfoText, clone];
    }
}
