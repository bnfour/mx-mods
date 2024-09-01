using System;
using System.Collections.Generic;
using System.Reflection;
using Bnfour.MusynxMods.SkinTweaks.Utilities;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch]
public class UI0X_ComboScriptComboComboPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(UI0A_ComboScript), "ComboCombo");
        yield return AccessTools.Method(typeof(UIE_ComboScript), "ComboCombo");
    }

    private static void Postfix(MonoBehaviour __instance)
    {
        // TODO this probably will interfere with a vanilla update
        // if a single note on a different track is tapped while holding a long note
        // (nothing breaking, i guess)
        var mod = Melon<SkinTweaksMod>.Instance;
        if (mod.LongNotesEndScoring && mod.KnockingCounter.AnyLongNotes)
        {
            var type = __instance is UI0A_ComboScript ? typeof(UI0A_LongNoteScript)
                : __instance is UIE_ComboScript ? typeof(UI0E_LongNoteScript)
                : throw new ApplicationException("Unsupported skin's ComboScript is passed");

            // TODO docs say this is a resource-hungry opeation, cache somehow?
            var obj = UnityEngine.Object.FindObjectOfType(type) as MonoBehaviour;

            mod.LoggerInstance.Msg($"null {obj == null}");
            ScoreUpdater.Update(obj);
        }
    }
}
