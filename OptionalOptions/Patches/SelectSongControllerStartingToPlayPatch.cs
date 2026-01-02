using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.OptionalOptions.Patches;

/// <summary>
/// Patch that extends the starting animation to fit the backgroud moving animation,
/// if the options were skipped.
/// </summary>
// StartingToPlay is private, so nameof won't work :(
[HarmonyPatch(typeof(SelectSongController), "StartingToPlay")]
public class SelectSongControllerStartingToPlayPatch
{
    internal static void Prefix(ref float time)
    {
        if (Melon<OptionalOptionsMod>.Instance.SkippingOptions)
        {
            // default 0.4 is not enough for the bg to fully move
            //TODO tweak more?
            time = 0.725f;
        }
    }
}
