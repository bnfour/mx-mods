using System.Collections.Generic;
using System.Reflection;

using HarmonyLib;
using MelonLoader;

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
        var songInfoCore = _coreByIndex.Invoke(__instance, [__instance.songIndex]) as SongInfoCore;

        var provider = Melon<SongInfoMod>.Instance.songDataProvider;
        var data = provider.GetSongData(songInfoCore);

        if (data == null)
        {
            return;
        }

        var isBigMenu = __originalMethod.Name.Equals("ChangingSongBg");
        Melon<SongInfoMod>.Logger.Msg($"{data.Duration}, {data.Bpm} BPM{(data.HasSv ? " SV!" : string.Empty)} in {(isBigMenu ? "big" : "smol")} menu xdd");
    }
}
