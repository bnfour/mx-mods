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
        ["00_142102"] = "2",
        // Silence To Freeze EZ
        ["00_128401"] = "11",
        // Can't it be true EZ
        ["00_129001"] = "11",
        // Cipher : /2&//<|0 EZ
        ["00_130601"] = "11",
        // Red Rave EZ
        ["00_148001"] = "1",
        // Red Rave HD
        ["00_148002"] = "2",
        // Red Rave IN
        ["00_148003"] = "2",
    };

    internal static void Prefix(string[] songCoreStrs)
    {
        if (Replacements.ContainsKey(songCoreStrs[0]))
        {
            songCoreStrs[9] = Replacements[songCoreStrs[0]];
        }
    }
}
