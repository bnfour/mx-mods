using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

/// <summary>
/// A patch that disables mountains overlay just after it's enabled.
/// </summary>
[HarmonyPatch(typeof(UI0D_BgController), nameof(UI0D_BgController.GetSongBgSprite))]
public class UI0D_BgControllerGetSongBgSpritePatch
{
    private static void Postfix(UI0D_BgController __instance)
    {
        if (Melon<SkinTweaksMod>.Instance.MountainRemovalEnabled)
        {
            __instance.mountainController.SetActive(false);
        }
    }
}
