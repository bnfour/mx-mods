using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using HarmonyLib;
using MelonLoader;

using Bnfour.MusynxMods.PlentifulStats.Utilities;

namespace Bnfour.MusynxMods.PlentifulStats.Patches;

/// <summary>
/// Patch to rewrite the target method slightly to store the previous best score,
/// just before its possible change to a new high score. The stored value is then
/// shown on the stats screen for easy comparison.
/// </summary>
// the method is private unfortunately
[HarmonyPatch(typeof(SongInfoCore), "UpdateSync")]
public class SongInfoCoreUpdateSyncPatch
{
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            // keep all the original instructions
            yield return instruction;
            // if the last instruction was "call the getter of SongSaveInfo's SyncNumber",
            // the return value (that we want to store) is on top of the stack right now;
            // let's call our own helper that grabs it from here, stores it,
            // and returns the same value to the top of the stack as if nothing happened
            // to not break the rest of the original method
            if (instruction.opcode == OpCodes.Callvirt)
            {
                var methodInfo = instruction.operand as MethodInfo;
                if (methodInfo.DeclaringType.Name == nameof(SongSaveInfo)
                    && methodInfo.Name == "get_SyncNumber")
                {
                    yield return CodeInstruction.Call(typeof(SyncNumberHelper), nameof(SyncNumberHelper.Store), [typeof(int)]);
                }
            }
        }
    }
}
