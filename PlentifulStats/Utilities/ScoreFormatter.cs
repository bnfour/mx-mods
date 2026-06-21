namespace Bnfour.MusynxMods.PlentifulStats.Utilities;

public static class ScoreFormatter
{
    /// <summary>
    /// Formats the raw score value to outward-facing format.
    /// 12345 => "123.45%"
    /// </summary>
    /// <param name="raw">Actual score value.</param>
    /// <returns>Score formatted for user display.</returns>
    public static string FormatSyncNumber(int raw)
    => ((float)raw / 100).ToString("0.00") + "%";
}
