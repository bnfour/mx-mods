using System.Collections.Generic;

using HarmonyLib;

namespace Bnfour.MusynxMods.MenuTweaks.Patches;

/// <summary>
/// Fixes the frame ids in data just before it's consumed to create a
/// SongInfoCore instance.
/// </summary>
[HarmonyPatch(typeof(SongInfoCore), nameof(SongInfoCore.Create))]
public class SongInfoCoreCreatePatch
{
    // maps raw song id, as in songList_org.txt, including the dlc id probably
    // (or whatever the part before "_" is)
    // to corrected frame id, as a string to be parsed in a bit
    private static readonly Dictionary<string, string> Replacements = new()
    {
        // Anökumene EZ
        ["00_142101"] = "1",
        // Anökumene HD
        ["00_142102"] = "2"
    };

    internal static void Prefix(string[] songCoreStrs)
    {
        if (Replacements.ContainsKey(songCoreStrs[0]))
        {
            songCoreStrs[9] = Replacements[songCoreStrs[0]];
        }
    }
}
