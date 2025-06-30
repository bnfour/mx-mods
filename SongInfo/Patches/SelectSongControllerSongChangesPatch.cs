using System.Collections.Generic;
using System.Reflection;

using HarmonyLib;

namespace Bnfour.MusynxMods.SongInfo.Patches;

/// <summary>
/// A patch that is fired on song or difficulty change to update the custom UI.
/// </summary>
[HarmonyPatch]
internal class SelectSongControllerSongChangesPatch
{
    // saved for reuse, the original method is not directly accessible
    private static readonly MethodInfo _coreByIndex = typeof(SelectSongController).GetMethod("GetSongInfoCoreByIndex", BindingFlags.Instance | BindingFlags.NonPublic);

    internal static IEnumerable<MethodBase> TargetMethods()
    {
        // called when the "big" menu changes songs/difficulties
        yield return AccessTools.Method(typeof(SelectSongController), "ChangingSongBg");
        // called when the list menu changes songs/difficulties, seems to be called twice in a row when moving down
        yield return AccessTools.Method(typeof(SelectSongController), "ChangeShowSmall");
    }

    internal static void Postfix(SelectSongController __instance, MethodBase __originalMethod)
    {
        var songInfoCore = _coreByIndex.Invoke(__instance, [__instance.songIndex]);

        // TODO get data from cache or data by songInfoCore
        // SongInfoCore -> SongData? (returns null when data is n/a, like shop entry in the big menu)

        // update UI (which one is determined by method name)
        // maybe in a helper class as well?
        var isBigMenu = __originalMethod.Name.Equals("ChangingSongBg");
    }
}
