using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.QuickQuit.Utilities;

internal static class CanPauseChecker
{
    /// <summary>
    /// Checks if the song player's state indicates that it can pause.
    /// </summary>
    /// <param name="songPlayer">Song player instance to check.</param>
    /// <param name="traverse">Traverse that was used to check for the fields.
    /// An out parameter so it can be reused if the check succeeds to call other methods.</param>
    /// <returns>The value of songPlayer's canPause or canPauese, whichever exists.</returns>
    public static bool CanPause(MonoBehaviour songPlayer, out Traverse traverse)
    {
        // the gimmick here is that different song player classes use different field names:
        // - some (3D skins?) use proper "canPause"
        // - others (2D skins?) use mysterious "canPauese"
        //   (likely a typo that was copied)
        // so this method looks for both spellings

        if (!songPlayer.GetType().Name.EndsWith("_SongPlayer"))
        {
            throw new System.Exception("Not a Song player passed.");
        }

        traverse = Traverse.Create(songPlayer);
        var correctlySpelledField = traverse.Field("canPause");

        return (correctlySpelledField.FieldExists()
                ? correctlySpelledField
                : traverse.Field("canPauese"))
            .GetValue<bool>();
    }
}
