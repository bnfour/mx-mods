using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks;

public class SkinTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _longNotesEndScoringEnabled;

    internal bool LongNotesEndScoring => _longNotesEndScoringEnabled.Value;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_SkinTweaks");

        _longNotesEndScoringEnabled = _prefsCategory.CreateEntry("LongNoteEndScoring", true,
            "Update display score on long note end", "Updates the score display on UI0A when long notes end.");
    }
}
