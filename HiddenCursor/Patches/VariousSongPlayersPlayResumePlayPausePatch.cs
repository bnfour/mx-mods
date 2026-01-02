using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.HiddenCursor.Patches;

// that's a ridiculous name, but at least it's accurate-ish
/// <summary>
/// Handles game (un)pausing: shows the cursor when the game is paused,
/// hides it again on unpausing.
/// </summary>
[HarmonyPatch]
public class VariousSongPlayersPlayResumePlayPausePatch
{
    internal static IEnumerable<MethodBase> TargetMethods()
    {
        // basically, this patch is for both PlayPause and PlayResume methods
        // of all the UI<whatever>_SongPlayer classes, which seem to be not related

        return AccessTools.GetTypesFromAssembly(typeof(UI0_SongPlayer).Assembly)
            .Where(type => type.Name.StartsWith("UI") && type.Name.EndsWith("_SongPlayer"))
            .SelectMany(type => type.GetMethods())
            .Where(method => method.Name.StartsWith("Play") && method.ReturnType == typeof(void))
            .Cast<MethodBase>();
    }

    internal static void Postfix(MethodBase __originalMethod)
    {
        // PlayPause may return early without pausing the game,
        // notably before and after the actual song;
        // the easiest way to check if the game was actually paused across all
        // the different *_SongPlayer classes is to check the timescale
        // -- it's set to 0 when the game is paused
        var actuallyPaused = __originalMethod.Name == "PlayPause" && Time.timeScale == 0f;

        Cursor.visible = actuallyPaused;
    }
}
