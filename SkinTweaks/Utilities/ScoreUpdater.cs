using System;
using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.SkinTweaks.Utilities;

/// <summary>
/// A helper class to incapsulate score update logic.
/// </summary>
internal static class ScoreUpdater
{
    // this is yet another separate reimplementation of the score counting
    // (ScoreScript only has a method to add a new _note_ to the score, yet only the combo is updated in this case)
    // hopefully, the score calculations are pretty straightforward

    /// <summary>
    /// Recalculates the score for the latest state of the game,
    /// and calls a method to update the display.
    /// </summary>
    /// <param name="parentScript">A LongNoteScript from which mod's patches were fired.</param>
    internal static void Update(MonoBehaviour parentScript)
    {
        // different skins have different lengths of display score,
        // 9 for UI0A (Techno2D), 6 for UI0E (STG2D)
        var multiplier = parentScript switch
        {
            UI0A_LongNoteScript => 100_000_000f,
            UI0E_LongNoteScript => 100_000f,
            _ => throw new ApplicationException("Unsupported skin's LongNoteScript is passed")
        };

        // extract the data necessary for score calculation
        var scoreScriptField = Traverse.Create(parentScript).Field("scoreScript");

        var accuracyCount = scoreScriptField.Field("accuracyCount").GetValue<int>();
        var baseAccuracy = scoreScriptField.Field("baseAccuracy").GetValue<float>();
        var exAccuracy = scoreScriptField.Field("exAccuracy").GetValue<float>();

        var totalCombo = BMSLib.JudgeGrade.TotalCombo;
        var maxCombo = Traverse.Create(parentScript).Field("comboScript").Method("ScoreGetMaxCombo").GetValue<int>();

        // the score is always recalculated from scratch, this class is no exception
        var num = (Math.Min(baseAccuracy / accuracyCount, 1f)
                + Math.Min(exAccuracy / accuracyCount, 0.15f)
                + Math.Min(maxCombo / (float)totalCombo / 10f, 0.1f))
            * multiplier;

        // use the original score script to update the UI
        scoreScriptField.Field("scoreNum").SetValue(num);
        scoreScriptField.Method("ChangeMarkNumberRens").GetValue();
    }
}
