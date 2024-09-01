using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch]
public class UI0X_LongNoteScriptKnockingTrackerPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(UI0A_LongNoteScript), nameof(UI0A_LongNoteScript.LiftUp));
        yield return AccessTools.Method(typeof(UI0E_LongNoteScript), nameof(UI0E_LongNoteScript.LiftUp));
        // TODO remove? these are fired at a random (to me, i'm sure there's a logic i don't get) moments, driving the value to negative
        // yield return AccessTools.Method(typeof(UI0A_LongNoteScript), nameof(UI0A_LongNoteScript.Reset));
        // yield return AccessTools.Method(typeof(UI0E_LongNoteScript), nameof(UI0E_LongNoteScript.Reset));

        yield return AccessTools.Method(typeof(UI0A_LongNoteScript), nameof(UI0A_LongNoteScript.Knock));
        yield return AccessTools.Method(typeof(UI0E_LongNoteScript), nameof(UI0E_LongNoteScript.Knock));
    }

    private static void Postfix(MethodBase __originalMethod, bool ___knocking)
    {
        var counter = Melon<SkinTweaksMod>.Instance.KnockingCounter;
        // TODO express this better: pressing a button when a long note is coming up in a track actually fires Knock,
        // but does not set knocking, unless actual note was triggered
        // (my habit of spamming keys on rests is useful for once -- i noticed this early)
        var delta = __originalMethod.Name == "Knock"
            ? (___knocking ? 1 : 0)
            : -1;
        counter.Count += delta;
    }
}
