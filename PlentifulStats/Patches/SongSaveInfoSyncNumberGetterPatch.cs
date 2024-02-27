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
        // WorkingInstance being not null implies that the feature is turned on,
        // see the patch that sets it -- no need to check mod's PrevBest again
        if (modInstance.WorkingInstance != null && __instance == modInstance.WorkingInstance)
        {
            modInstance.SyncNumber = __result;
        }
    }
}
