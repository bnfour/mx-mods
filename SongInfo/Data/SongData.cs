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

    // TODO comments

    public Hashtable SerializeForMiniJson()
        => new()
        {
            [nameof(Bpm)] = Bpm,
            [nameof(Duration)] = Duration,
            [nameof(HasSv)] = HasSv
        };

    public static SongData DeserializeFromMiniJson(Hashtable hashtable)
    {
        // TODO check keys, throw on mismatch
        return new()
        {
            Bpm = (string)hashtable[nameof(Bpm)],
            Duration = (string)hashtable[nameof(Duration)],
            HasSv = (bool)hashtable[nameof(HasSv)]
        };
    }
}
