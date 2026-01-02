using MelonLoader;

namespace Bnfour.MusynxMods.PlentifulStats;

public class PlentifulStatsMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _rToRestartEnabled;
    private MelonPreferences_Entry<bool> _separateExactsEnabled;
    private MelonPreferences_Entry<bool> _prevBestEnabled;
    private MelonPreferences_Entry<bool> _nextButtonFixEnabled;
    // values to check in patches
    internal bool RToRestart => _rToRestartEnabled.Value;
    internal bool SeparateExacts => _separateExactsEnabled.Value;
    internal bool PrevBest => _prevBestEnabled.Value;
    internal bool EnNextFix => _nextButtonFixEnabled.Value;

    /// <summary>
    /// Stores the previous high score for a song to display on the stats screen
    /// if <see cref="PrevBest"/> is enabled.
    /// </summary>
    internal int SyncNumber { get; set; }

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_PlentifulStats");

        _rToRestartEnabled = _prefsCategory.CreateEntry("RToRestart", true,
            "Enable R to restart", "Enables R key to restart from the stats screen.");
        _separateExactsEnabled = _prefsCategory.CreateEntry("SeparateExacts", true,
            "Separate exacts count", "Displays separate blue and cyan exacts counts.");
        _prevBestEnabled = _prefsCategory.CreateEntry("PrevBest", true,
            "Previous best", "Displays previous best score at the stats screen.");
        _nextButtonFixEnabled = _prefsCategory.CreateEntry("EnNextFix", true,
            "Next button fix", "Fixes Next button switching to Chinese on 120+ scores.");

        if (!RToRestart && !SeparateExacts && !PrevBest && !EnNextFix)
        {
            LoggerInstance.Warning("No features of the mod enabled, it can be uninstalled.");
        }
    }
}
