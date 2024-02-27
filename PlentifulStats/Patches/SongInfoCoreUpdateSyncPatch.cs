using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Patch to store a reference to SongSaveInfo wwe need to get data from.
/// SongSaveInfo.SyncNumber' getter is called a _lot_ internally, and we're only interested
/// in one specific call inside the method this patch is for.
/// So we just keep a reference to check against in the other patch
/// while this method is executed.
/// </summary>
// the method is private unfortunately
[HarmonyPatch(typeof(SongInfoCore), "UpdateSync")]
public class SongInfoCoreUpdateSyncPatch
{
    internal static void Prefix(SongSaveInfo ___saveInfo)
    {
        var modInstance = Melon<PlentifulStatsMod>.Instance;
        if (modInstance.PrevBest)
        {
            modInstance.WorkingInstance = ___saveInfo;
        }
    }

    internal static void Postfix()
    {
        Melon<PlentifulStatsMod>.Instance.WorkingInstance = null;
    }
}
