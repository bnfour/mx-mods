using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.QuickQuit.Utilities;

internal static class SongPlayersHelper
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

        traverse = Traverse.Create(songPlayer);
        var correctlySpelledField = traverse.Field("canPause");

        return (correctlySpelledField.FieldExists()
                ? correctlySpelledField
                : traverse.Field("canPauese"))
            .GetValue<bool>();
    }

    /// <summary>
    /// Restarts the song player's scene.
    /// </summary>
    /// <param name="songPlayerTraverse">Traverse to use; supposed to be provided
    /// from <see cref="CanPause"/> called previously.</param>
    public static void Restart(Traverse songPlayerTraverse)
        => CallSceneChange(songPlayerTraverse, "Restart");

    /// <summary>
    /// Quits the song player's scene.
    /// </summary>
    /// <param name="songPlayerTraverse">Traverse to use; supposed to be provided
    /// from <see cref="CanPause"/> called previously.</param>
    public static void Quit(Traverse songPlayerTraverse)
        => CallSceneChange(songPlayerTraverse, "BackToSelectScene");

    /// <summary>
    /// Internal method for scene changes. Does some extra work for smoother transition,
    /// same for quit and restart -- because these are both scene changes.
    /// </summary>
    /// <param name="songPlayerTraverse">Traverse to use; supposed to be provided
    /// from <see cref="CanPause"/> called previously.</param>
    /// <param name="methodName">Method to use: "Restart" or "BackToSelectScene".</param>
    private static void CallSceneChange(Traverse songPlayerTraverse, string methodName)
    {
        // stop the BGM playing through the scene transition
        // thankfully, the naming for this one is consistent
        songPlayerTraverse.Field("bgmAudioSourceCri").Method("Stop").GetValue();

        // call the actual scene change
        songPlayerTraverse.Method(methodName).GetValue();
    }
}
