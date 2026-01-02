using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.OptionalOptions.Patches;


/// <summary>
/// Patch to remove the darkening of the screen on switching to the options screen
/// if it's skipped. The whole transition looks nicer this way.
/// </summary>
// DimingDefficultButton [sic] is private, so nameof won't work :(
[HarmonyPatch(typeof(SelectSongController), "DimingDefficultButton")]
public class SelectSongControllerDimingDefficultButttonPatch
{
    internal static void Prefix(SelectSongController __instance)
    {
        if (Melon<OptionalOptionsMod>.Instance.SkippingOptions)
        {
            // it should be okay to left this disabled,
            // as the scene is going to change momentarily;
            // the next time we'll going to see this renderer,
            // it will be re-instantiated with the rest of the select song scene
            __instance.enterBlackRenderer.enabled = false;
        }
    }
}
