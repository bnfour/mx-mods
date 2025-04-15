using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HarmonyLib;
using MelonLoader;
using UnityEngine;

using Bnfour.MusynxMods.QuickQuit.Utilities;

namespace Bnfour.MusynxMods.QuickQuit.Patches;

/// <summary>
/// Patch that calls for a quit or restart if shortcut keys are pressed
/// and the game can be paused (so it's not too early nor too late into the level).
/// </summary>
[HarmonyPatch]
public class VariousSongPlayersUpdatePatch
{
    internal static IEnumerable<MethodBase> TargetMethods()
    {
        // all Update methods of all UI<whatever>_SongPlayer classes there is
        return AccessTools.GetTypesFromAssembly(typeof(UI0_SongPlayer).Assembly)
            .Where(type => type.Name.StartsWith("UI") && type.Name.EndsWith("_SongPlayer"))
            // Update is not a public method, so binding flags are required
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
            .Where(method => method.Name.Equals("Update") && method.ReturnType == typeof(void))
            .Cast<MethodBase>();
    }

    internal static void Postfix(MonoBehaviour __instance)
    {
        var input = Melon<QuickQuitMod>.Instance.ExtraInput;
        Traverse traverse;

        if (input.RestartDown && SongPlayersHelper.CanPause(__instance, out traverse))
        {
            SongPlayersHelper.Restart(traverse);
        }
        else if (input.QuitDown && SongPlayersHelper.CanPause(__instance, out traverse))
        {
            SongPlayersHelper.Quit(traverse);
        }
    }
}
