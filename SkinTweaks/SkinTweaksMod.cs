using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks;

public class SkinTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _mountainRemovalEnabled;
    private MelonPreferences_Entry<bool> _longNotesEndScoringEnabled;

    internal bool MountainRemovalEnabled => _mountainRemovalEnabled.Value;
    internal bool LongNotesEndScoring => _longNotesEndScoringEnabled.Value;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_SkinTweaks");

        _mountainRemovalEnabled = _prefsCategory.CreateEntry("MountainRemoval", true,
            "Mountain removal", "Removes moving white mountain overlays from Ink2D.");
        _longNotesEndScoringEnabled = _prefsCategory.CreateEntry("LongNoteEndScoring", true,
            "Update score display for long notes", "Updates the score display on Techno2D and STG2D when long notes end.");

        if (!MountainRemovalEnabled && !LongNotesEndScoring)
        {
            LoggerInstance.Warning("No features of the mod enabled, it can be unistalled.");
        }
    }
}
