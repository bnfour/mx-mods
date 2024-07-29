using Bnfour.MusynxMods.SkinTweaks.Utilities;
using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch(typeof(UI0A_LongNoteScript), "EndKnock")]
public class UI0A_LongNoteScriptEndKnockPatch
{
    // this method is called a lot of times,
    // but only does some work once when UI0A_LongNoteScript.endKnock is false,
    // setting it to true in the process

    // so in order to this patch to match this behaviour,
    // we need to monitor the value of that flag as well

    private static void Prefix(UI0A_LongNoteScript __instance, out bool __state)
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

    private static void Postfix(UI0A_LongNoteScript __instance, bool __state, bool ___knocking)
    {
        // whether the feature is enabled at all is checked by prefix,
        // __state being true means the actual method did nothing and/or this feature is disabled;
        // UI0A_LongNoteScript.knocking is also checked: if it's false, the note was dropped mid-way,
        // and another patch already updated the UI with relevant score value
        if (!__state && ___knocking)
        {
            ScoreUpdater.Update(__instance);
        }
    }
}
