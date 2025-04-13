using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.QuickQuit.Patches;

/// <summary>
/// Patch that calls for a quit or restart if shortcut keys are pressed
/// and the game can be paused (so it's not too early nor too late into the level).
/// </summary>
[HarmonyPatch]
public class VariousSongPlayersUpdatePatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        // all Update methods of all UI*whatever*_SongPlayer classes there is
        return AccessTools.GetTypesFromAssembly(typeof(UI0_SongPlayer).Assembly)
            .Where(type => type.Name.StartsWith("UI") && type.Name.EndsWith("_SongPlayer"))
            // Update is not a public method
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
            .Where(method => method.Name.Equals("Update") && method.ReturnType == typeof(void))
            .Cast<MethodBase>();
    }

    private static void Postfix(MonoBehaviour __instance)
    {
        var input = Melon<QuickQuitMod>.Instance.ExtraInput;

        if (input.RestartDown)
        {
            // TODO stuff
        }

        if (input.QuitDown)
        {
            // TODO stuff
        }
    }
}
