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

        yield return AccessTools.Method(typeof(UI0A_LongNoteScript), nameof(UI0A_LongNoteScript.Knock));
        yield return AccessTools.Method(typeof(UI0E_LongNoteScript), nameof(UI0E_LongNoteScript.Knock));
    }

    private static void Postfix(MethodBase __originalMethod, bool ___knocking)
    {
        var counter = Melon<SkinTweaksMod>.Instance.KnockingCounter;
        if (counter == null)
        {
            return;
        }
        // knock is called many times, we only care when it leaves knocking flag set
        if (__originalMethod.Name == nameof(UI0A_LongNoteScript.Knock))
        {
            if (___knocking)
            {
                counter?.StartKnock();
            }
        }
        else
        {
            counter?.EndKnock();
        }
    }
}
