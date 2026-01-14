using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Patch that adds R key bind to the group that handles game restart,
/// if the feature is enabled. Works either on stats screen or in-game pause menu.
/// </summary>
[HarmonyPatch(typeof(InputBlendGroup), MethodType.Constructor, [ typeof(InputSetting[]) ])]
public class InputBlendGroupConstructorPatch
{
    internal static void Prefix(ref InputSetting[] blendSetting)
    {
        if (Melon<PlentifulStatsMod>.Instance.RToRestart
            && blendSetting.Length == 3
            && blendSetting[0].code == UnityEngine.KeyCode.F1)
        {
            blendSetting = blendSetting.AddToArray(new InputSetting
            {
                type = ButtonType.keyCode,
                code = UnityEngine.KeyCode.R,
                device = InputDeviceType.keyboard
            });
        }
    }
}
