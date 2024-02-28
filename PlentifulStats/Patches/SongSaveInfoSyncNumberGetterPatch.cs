using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Patch to get the last value of the song just before it might be overwritten
/// by a new record (the last value is supposed to be shown alongside it).
/// </summary>
// the property itself is internal, so no nameof available
[HarmonyPatch(typeof(SongSaveInfo), "SyncNumber", MethodType.Getter)]
public class SongSaveInfoSyncNumberGetterPatch
{
    internal static void Postfix(int __result, SongSaveInfo __instance)
    {
        var modInstance = Melon<PlentifulStatsMod>.Instance;
        // SongId being not null implies PrevBest being true (see SongInfoCoreUpdateSyncPatch),
        // no need to check it here again
        if (modInstance.SongId.HasValue)
        {
            var songId = Traverse.Create(__instance).Property<int>("SongId").Value;
            if (songId == modInstance.SongId.Value)
            {
                modInstance.SyncNumber = __result;
            }
        }
    }
}
