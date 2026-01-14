using System.Collections.Generic;
using System.Linq;

using HarmonyLib;

namespace Bnfour.MusynxMods.MenuTweaks.Patches;

[HarmonyPatch(typeof(SelectSongController), nameof(SelectSongController.SmallChangeDefficultInf))]
// (original spelling of "Defficult" preserved)
public class SelectSongControllerSmallChangeDefficultInfPatch
{
    internal static bool Prefix(SelectSongController __instance,
        // contains normal difficulty SongInfoCores when the method is called
        List<SongInfoCore> ___songInfoCoreList,
        // always contains inferno difficulty SongInfoCores (for the current key mode?)
        List<SongInfoCore> ___songInfoCoreInList)
    {
        // the fact that the needed methods and properties are internal or private
        // is extremely unfortunate

        var normalizedIndex = Traverse.Create(__instance)
            .Method("GetSongIndex", [typeof(int)])
            .GetValue<int>(__instance.songIndex);

        var currentRealName = Traverse.Create(___songInfoCoreList[normalizedIndex])
            .Property("RealName")
            .GetValue<string>();

        var hasInfernoDifficulty = ___songInfoCoreInList
            .Select(sic => Traverse.Create(sic).Property("RealName").GetValue<string>())
            .Any(realName => realName == currentRealName);

        return hasInfernoDifficulty;
    }
}
