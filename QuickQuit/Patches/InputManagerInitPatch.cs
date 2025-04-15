using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.QuickQuit.Patches;

/// <summary>
/// A patch that just calls the similar initializtion method for the custom input
/// whenever the original InputManager is initialized.
/// </summary>
[HarmonyPatch(typeof(InputManager), "Init")]
public class InputManagerInitPatch
{
    internal static void Postfix()
    {
        Melon<QuickQuitMod>.Instance.ExtraInput.Init();
    }
}
