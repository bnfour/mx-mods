using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

using Bnfour.MusynxMods.SkinTweaks.Utilities;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

/// <summary>
/// A patch that caches a script to use, and calls the display score updates
/// every combo tick.
/// </summary>
[HarmonyPatch]
public class UI0X_ComboScriptComboComboPatch
{
    internal static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(UI0A_ComboScript), "ComboCombo");
        // the zero from UI0E is missing for some reason,
        // actual scripts use this as well, so it _should_ be working
        yield return AccessTools.Method(typeof(UIE_ComboScript), "ComboCombo");
    }

    internal static void Postfix(MonoBehaviour __instance)
    {
        var mod = Melon<SkinTweaksMod>.Instance;
        if (mod.LongNotesCustomScoring && mod.LongNotesAdvancedCustomScoring
            // LongNotesAdvancedCustomScoring implies KnockingCounter being non-null
            && mod.KnockingCounter.AnyLongNotes)
        {
            var longNoteScript = mod.LastKnownLongNoteScript;
            if (longNoteScript == null)
            {
                var type = __instance switch
                {
                    UI0A_ComboScript => typeof(UI0A_LongNoteScript),
                    UIE_ComboScript => typeof(UI0E_LongNoteScript),
                    _ => throw new ApplicationException("Unsupported skin's ComboScript is passed"),
                };
                longNoteScript = UnityEngine.Object.FindObjectOfType(type) as MonoBehaviour;
            }
            ScoreUpdater.Update(longNoteScript);
        }
    }
}
