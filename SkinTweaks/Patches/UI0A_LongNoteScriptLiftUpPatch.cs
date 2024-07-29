using Bnfour.MusynxMods.SkinTweaks.Utilities;
using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch(typeof(UI0A_LongNoteScript), nameof(UI0A_LongNoteScript.LiftUp))]
public class UI0A_LongNoteScriptLiftUpPatch
{
    // LiftUp method is called once when a long note was played, but is dropped
    // before its actual end, so some of the combo will end up missing
    
    // this will reset UI0A_LongNoteScript.knocking flag -- we use this fact
    // in another patch

    private static void Postfix(UI0A_LongNoteScript __instance)
    {
        if (Melon<SkinTweaksMod>.Instance.LongNotesEndScoring)
        {
            ScoreUpdater.Update(__instance);
        }
    }
}
