using System;
using System.Collections;

namespace Bnfour.MusynxMods.SongInfo.Data;

/// <summary>
/// Data structure to hold cached data about a song.
/// </summary>
public record SongData
{
    /// <summary>
    /// Song's BPM, preformatted as a string. If BPM changes during the map,
    /// this a minimum and maximum value separated by "~", like "1~999"
    /// </summary>
    public string Bpm { get; set; }

    /// <summary>
    /// Song duration, preformatted to "mm:ss" like "01:34".
    /// </summary>
    public string Duration { get; set; }

    /// <summary>
    /// Indicates if the map has non-BPM scroll speed changes.
    /// </summary>
    public bool HasSv { get; set; }

    #region serialization

    /// <summary>
    /// Converts the data in a record to a serialization-friendly format.
    /// </summary>
    /// <returns>A hashtable representation of public fields.</returns>
    public Hashtable SerializeForMiniJson()
        => new()
        {
            [nameof(Bpm)] = Bpm,
            [nameof(Duration)] = Duration,
            [nameof(HasSv)] = HasSv
        };

    /// <summary>
    /// Creates an instance from a serialization representation.
    /// </summary>
    /// <param name="hashtable">Object to get parameters from.</param>
    /// <returns>An instance with the data from the hashtable.</returns>
    public static SongData DeserializeFromMiniJson(Hashtable hashtable)
    {
        if (hashtable.Keys.Count != 3
            || !(hashtable.ContainsKey(nameof(Bpm)) && hashtable.ContainsKey(nameof(Duration)) && hashtable.ContainsKey(nameof(HasSv))))
        {
            throw new ApplicationException("Missing and/or unexpected fields in hashtable");
        }

        return new()
        {
            Bpm = (string)hashtable[nameof(Bpm)],
            Duration = (string)hashtable[nameof(Duration)],
            HasSv = (bool)hashtable[nameof(HasSv)]
        };
    }

    #endregion
}
