using System.Collections.Generic;
using System.Reflection;
using Bnfour.MusynxMods.SkinTweaks.Utilities;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch]
public class UI0X_LongNoteScriptEndKnockPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(UI0A_LongNoteScript), "EndKnock");
        yield return AccessTools.Method(typeof(UI0E_LongNoteScript), "EndKnock");
    }

    // this method is called a lot of times,
    // but only does some work once when UI0*_LongNoteScript.endKnock is false,
    // setting it to true in the process

    // so in order to this patch to match this behaviour,
    // we need to monitor the value of that flag as well

    private static void Prefix(MonoBehaviour __instance, out bool __state)
    {
        if (Melon<SkinTweaksMod>.Instance.LongNotesEndScoring)
        {
            __state = Traverse.Create(__instance).Field<bool>("endKnock").Value;
        }
        else
        {
            // tell postfix to do nothing
            __state = true;
        }
    }

    private static void Postfix(MonoBehaviour __instance, bool __state, bool ___knocking)
    {
        // whether the feature is enabled at all is checked by prefix,
        // __state being true means the actual method did nothing and/or this feature is disabled;
        // UI0*_LongNoteScript.knocking is also checked: if it's false, the note was dropped mid-way,
        // and another patch already updated the UI with relevant score value
        if (!__state && ___knocking)
        {
            ScoreUpdater.Update(__instance);
            Melon<SkinTweaksMod>.Instance.KnockingCounter?.EndKnock();
        }
    }
}
