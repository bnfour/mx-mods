using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.VSyncAnnihilator.Patches;

/// <summary>
/// Patch that denies any changes to properties specified,
/// unless they come from within this mod.
/// </summary>
[HarmonyPatch]
public class SetterIgnorerPatch
{
    internal static IEnumerable<MethodBase> TargetMethods()
    {
        // there are some assignments to targetFrameRate of 60 and vSyncCount of 1 in the code,
        // and we want to ignore these to keep custom settings
        yield return AccessTools.PropertySetter(typeof(Application), nameof(Application.targetFrameRate));
        yield return AccessTools.PropertySetter(typeof(QualitySettings), nameof(QualitySettings.vSyncCount));
    }

    internal static bool Prefix()
    {
        return Melon<VSyncAnnihilatorMod>.Instance.ApproveChange;
    }
}
