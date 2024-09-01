using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

using Bnfour.MusynxMods.SkinTweaks.Utilities;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch]
public class UI0X_ComboScriptComboComboPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(UI0A_ComboScript), "ComboCombo");
        // the zero from UI0E is missing for some reason,
        // actual scrips use this as well, so it _should_ be working
        yield return AccessTools.Method(typeof(UIE_ComboScript), "ComboCombo");
    }

    private static void Postfix(MonoBehaviour __instance)
    {
        var mod = Melon<SkinTweaksMod>.Instance;
        if (mod.LongNotesCustomScoring && mod.LongNotesAdvancedCustomScoring
            // LongNotesAdvancedCustomScoring implies KnockingCounter being non-null
            && mod.KnockingCounter.AnyLongNotes)
        {
            var longNoteScript = mod.LastKnownLongNoteScript;
            if (longNoteScript == null)
            {
                var type = __instance is UI0A_ComboScript ? typeof(UI0A_LongNoteScript)
                    : __instance is UIE_ComboScript ? typeof(UI0E_LongNoteScript)
                    : throw new ApplicationException("Unsupported skin's ComboScript is passed");

                longNoteScript = UnityEngine.Object.FindObjectOfType(type) as MonoBehaviour;
            }
            ScoreUpdater.Update(longNoteScript);
        }
    }
}
