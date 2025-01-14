using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.MenuTweaks.Patches;

/// <summary>
/// A patch that mutes non-music menu sounds, if the mod configured to do so.
/// </summary>
[HarmonyPatch]
public class SoundEffectScriptPlayStuffPatch
{
    internal static IEnumerable<MethodBase> TargetMethods()
    {
        // in an improbable turn of events, all current (2025-01)
        // non-music-preview sound methods have names starting with "PlayS",
        // like "PlaySwitchSound"
        return typeof(SoundEffectScript).GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(method => method.Name.StartsWith("PlayS"))
            .Cast<MethodBase>();
    }

    internal static bool Prefix()
    {
        return !Melon<MenuTweaksMod>.Instance.MenuMuteEnabled;
    }
}
