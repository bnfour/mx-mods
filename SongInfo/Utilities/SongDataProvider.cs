using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HarmonyLib;
using UnityEngine;

using Bnfour.MusynxMods.SongInfo.Data;

namespace Bnfour.MusynxMods.SongInfo.Utilities;

// a convenience shortcut
using CacheType = SortedDictionary<int, SongData>;

/// <summary>
/// A class that manages extracting and storing song data.
/// </summary>
public class SongDataProvider
{

    private const string _cacheFilename = "song_info_cache.json";

    /// <summary>
    /// Saves the data received by parsing game files for reuse.
    /// </summary>
    private CacheType Cache => _cache ??= LoadCache();

    /// <summary>
    /// A reference to the commonly used static class, originally unavailable outside of the game assembly.
    /// </summary>
    private UserMemory UserMemory
        => _userMemory ??= typeof(UserMemory).GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) as UserMemory;

    /// <summary>
    /// Backing field for the UserMemory instance for lazy instantiation.
    /// </summary>
    private static UserMemory _userMemory;

    /// <summary>
    /// Backing field for the cache for lazy instantiation.
    /// </summary>
    private CacheType _cache;

    /// <summary>
    /// If set, the cache will be re-saved to the file to persist between sessions.
    /// </summary>
    private bool _somethingAdded = false;

    /// <summary>
    /// Access point to the class. Used to retrieve the song data (maybe cached).
    /// </summary>
    /// <param name="songInfoCore">Used to identify the song.</param>
    /// <returns>Data, or null if no data should be shown.</returns>
    public SongData GetSongData(SongInfoCore songInfoCore)
    {
        var songId = Traverse.Create(songInfoCore).Property("SongId").GetValue<int>();

        // 0 is shop entry that has no data associated
        if (songId == 0)
        {
            return null;
        }

        if (Cache.ContainsKey(songId))
        {
            return Cache[songId];
        }
        else
        {
            _somethingAdded = true;
            var newInfo = GetDataFromFiles(songInfoCore);
            Cache[songId] = newInfo;
            return newInfo;
        }
    }

    /// <summary>
    /// Gets song data from the game files. 
    /// </summary>
    /// <remarks>
    /// Generally fast enough to be used every time (unlike that other game),
    /// but still wrapped in a file cache to avoid reloading game resources over and over.
    /// </remarks>
    /// <param name="songInfoCore">The song to get data for.</param>
    /// <returns>Data to show and to save into the cache.</returns>
    private SongData GetDataFromFiles(SongInfoCore songInfoCore)
    {
        var traverse = Traverse.Create(songInfoCore);

        // getting the duration is straightforward, because it's available directly in the songInfoCore
        var rawDuration = traverse.Field("songLength").GetValue<int>();
        var formattedDuration = $"{rawDuration / 60:00}:{rawDuration % 60:00}";

        // to get bpm and sv, we need to parse the actual map file:
        // first, we construct its path
        // 4 or 6, obviously
        var buttonNumber = Traverse.Create(UserMemory).Field("buttonNumber").GetValue<int>();
        var bmsCoreName = traverse.Field("bmsCoreName").GetValue<string>();
        // difficulty indicator
        var hardMode = traverse.Field("hardMode").GetValue<string>();

        var resourcePath = $"Song/{bmsCoreName}/{bmsCoreName}{buttonNumber}T_{hardMode}";

        var asset = (TextAsset)Resources.Load(resourcePath);
        var lines = asset.text.Split('\n');
        // there are two type of bpm-related line types:
        // - just "BPM" seems to set the original bpm in the beginning of the file
        // - "BPMChanger" is used to change the bpm mid-level
        // both of those have actual bpm as last tab-separated parameter (changer also has a time(?) value)
        var bpms = lines
            .Where(l => l.StartsWith("BPM"))
            .Select(l => float.Parse(l.Split('\t').Last()));
        var minBpm = bpms.Min();
        var maxBpm = bpms.Max();
        var formattedBpm = minBpm == maxBpm ? $"{minBpm}" : $"{minBpm}~{maxBpm}";

        var hasSv = lines.Any(l => l.StartsWith("SpeedChange"));

        return new SongData
        {
            Bpm = formattedBpm,
            Duration = formattedDuration,
            HasSv = hasSv
        };
    }

    /// <summary>
    /// Loads existing cache from file, or returns an empty dict to use if file is not present.
    /// </summary>
    /// <returns>Existing cache, may be empty.</returns>
    private CacheType LoadCache()
    {
        // TODO load
        return [];
    }

    /// <summary>
    /// Saves the current cache to the file if it was extended this session.
    /// </summary>
    public void SaveCache()
    {
        if (_somethingAdded)
        {
            // TODO if new values were inserted, serialize the cache to the file.
        }
    }
}
