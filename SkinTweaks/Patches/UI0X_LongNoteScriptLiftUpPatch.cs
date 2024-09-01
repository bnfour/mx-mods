using System.Collections.Generic;
using System.Reflection;
using Bnfour.MusynxMods.SkinTweaks.Utilities;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch]
public class UI0X_LongNoteScriptLiftUpPatch
{
    // LiftUp method is called once when a long note was played, but is dropped
    // before its actual end, so some of the combo will end up missing
    
    // this will reset UI0*_LongNoteScript.knocking flag -- we use this fact
    // in another patch

    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(UI0A_LongNoteScript), nameof(UI0A_LongNoteScript.LiftUp));
        yield return AccessTools.Method(typeof(UI0E_LongNoteScript), nameof(UI0E_LongNoteScript.LiftUp));
    }

    private static void Postfix(MonoBehaviour __instance)
    {
        if (Melon<SkinTweaksMod>.Instance.LongNotesCustomScoring)
        {
            ScoreUpdater.Update(__instance);
        }
    }
}
