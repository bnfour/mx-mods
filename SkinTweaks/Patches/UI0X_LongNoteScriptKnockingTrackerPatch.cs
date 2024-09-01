using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

/// <summary>
/// Called when knocking flag of a LongNoteScript is meaningfully updated
/// to update the relevant counter.
/// </summary>
[HarmonyPatch]
public class UI0X_LongNoteScriptKnockingTrackerPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(UI0A_LongNoteScript), nameof(UI0A_LongNoteScript.LiftUp));
        yield return AccessTools.Method(typeof(UI0E_LongNoteScript), nameof(UI0E_LongNoteScript.LiftUp));

        yield return AccessTools.Method(typeof(UI0A_LongNoteScript), nameof(UI0A_LongNoteScript.Knock));
        yield return AccessTools.Method(typeof(UI0E_LongNoteScript), nameof(UI0E_LongNoteScript.Knock));
    }

    private static void Postfix(MonoBehaviour __instance, MethodBase __originalMethod, bool ___knocking)
    {
        var mod = Melon<SkinTweaksMod>.Instance;
        if (!(mod.LongNotesCustomScoring && mod.LongNotesAdvancedCustomScoring))
        {
            return;
        }

        var counter = mod.KnockingCounter;
        // knock is called many times, we only care when it leaves knocking flag set
        if (__originalMethod.Name == nameof(UI0A_LongNoteScript.Knock))
        {
            if (___knocking)
            {
                mod.LastKnownLongNoteScript = __instance;
                counter?.StartKnock();
            }
        }
        else
        {
            counter?.EndKnock();
        }
    }
}
