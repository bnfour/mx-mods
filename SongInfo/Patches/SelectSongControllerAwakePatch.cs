using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.SongInfo.Patches;

[HarmonyPatch(nameof(SelectSongController), "Awake")]
public class SelectSongControllerAwakePatch
{
    // TODO tweak position a bit?
    private static readonly Vector3 _offset = new(-475, 10, 0);

    internal static void Postfix(SelectSongController __instance)
    {
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
