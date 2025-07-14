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
    // saved for reuse, the original methods are not directly accessible
    private static readonly MethodInfo _coreByIndex = typeof(SelectSongController).GetMethod("GetSongInfoCoreByIndex", BindingFlags.Instance | BindingFlags.NonPublic);
    private static readonly MethodInfo _sipsByIndex = typeof(SelectSongController).GetMethod("GetSongInfoPackageByIndex", BindingFlags.Instance | BindingFlags.NonPublic);

    internal static IEnumerable<MethodBase> TargetMethods()
    {
        // called when the "big" menu changes songs/difficulty
        yield return AccessTools.Method(typeof(SelectSongController), "ChangingSongBg");
        // called on 4K <-> 6K switches in the "big" menu
        yield return AccessTools.Method(typeof(SelectSongController), "ChangingButtonMode");
        // called when the list menu changes songs/difficulties/mode, seems to be called twice in a row when moving down
        yield return AccessTools.Method(typeof(SelectSongController), "ChangeShowSmall");
    }

    internal static void Postfix(SelectSongController __instance, MethodBase __originalMethod)
    {
        var songInfoCore = _coreByIndex.Invoke(__instance, [__instance.songIndex]) as SongInfoCore;
        var provider = Melon<SongInfoMod>.Instance.songDataProvider;
        var data = provider.GetSongData(songInfoCore);
        // TODO check for shop, it displays data somehow -- it's not supposed to though
        if (data == null)
        {
            return;
        }

        var msg = $"{data.Duration}, {data.Bpm} BPM{(data.HasSv ? " SV!" : string.Empty)}";

        var isBigMenu = __originalMethod.Name.StartsWith("Changing");
        if (isBigMenu)
        {
            var currentSips = _sipsByIndex.Invoke(__instance, [__instance.songIndex]) as SongInfoPackageScript;
            currentSips.newinfoText[currentSips.newinfoText.Length - 1].text = msg;
            currentSips.newinfoText[currentSips.newinfoText.Length - 2].text = msg;
        }
        else
        {
            Melon<SongInfoMod>.Logger.Msg($"soon: {msg}");
        }
    }
}
