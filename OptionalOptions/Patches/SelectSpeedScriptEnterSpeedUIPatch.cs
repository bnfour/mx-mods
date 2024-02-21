using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.OptionalOptions.Patches;

/// <summary>
/// Patch that actually skips the options screen, unless Shift is held.
/// </summary>
[HarmonyPatch(typeof(SelectSpeedScript), nameof(SelectSpeedScript.EnterSpeedUI))]
public class SelectSpeedScriptEnterSpeedUIPatch
{
    private static bool Prefix(SelectSpeedScript __instance)
    {
        var mod = Melon<OptionalOptionsMod>.Instance;
        if (mod.ShiftDown)
        {
            // proceed as usual, mod does nothing
            mod.SkippingOptions = false;
            return true;
        }

        mod.SkippingOptions = true;
        __instance.selectsong.StartSong();
        return false;
    }
}
