using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.VSyncAnnihilator.Patches;

// both methods are only used by Harmony itself, not by anything in the assembly
#pragma warning disable IDE0051

/// <summary>
/// Patch that denies any changes to properties specified,
/// unless they come from within this mod.
/// </summary>
[HarmonyPatch]
public class SetterIgnorerPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        // there are some assignments to targetFrameRate of 60 and vSyncCount of 1 in the code,
        // and we want to ignore these to keep custom settings
        yield return AccessTools.PropertySetter(typeof(Application), nameof(Application.targetFrameRate));
        yield return AccessTools.PropertySetter(typeof(QualitySettings), nameof(QualitySettings.vSyncCount));
    }

    private static bool Prefix()
    {
        return Melon<VSyncAnnihilatorMod>.Instance.ApproveChange;
    }
}

#pragma warning restore
