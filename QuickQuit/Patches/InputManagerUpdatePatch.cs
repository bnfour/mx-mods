using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.QuickQuit.Patches;

/// <summary>
/// A patch that just calls the similar update method for the custom input
/// whenever the original InputManager updates.
/// </summary>
[HarmonyPatch(typeof(InputManager), "Update")]
public class InputManagerUpdatePatch
{
    internal static void Postfix()
    {
        Melon<QuickQuitMod>.Instance.ExtraInput.Update();
    }
}
