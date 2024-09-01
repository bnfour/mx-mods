using Bnfour.MusynxMods.SkinTweaks.Utilities;
using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks;

public class SkinTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _mountainRemovalEnabled;
    private MelonPreferences_Entry<bool> _longNotesCustomScoringEnabled;
    private MelonPreferences_Entry<bool> _longNotesAdvancedCustomScoringEnabled;

    public KnockingCounter KnockingCounter;

    internal bool MountainRemovalEnabled => _mountainRemovalEnabled.Value;
    internal bool LongNotesCustomScoring => _longNotesCustomScoringEnabled.Value;
    internal bool LongNotesAdvancedCustomScoring => _longNotesAdvancedCustomScoringEnabled.Value;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_SkinTweaks");

        _mountainRemovalEnabled = _prefsCategory.CreateEntry("MountainRemoval", true,
            "Mountain removal", "Removes moving white mountain overlays from Ink2D.");
        _longNotesCustomScoringEnabled = _prefsCategory.CreateEntry("LongNoteScoring", true,
            "Update score display for long notes", "Updates the score display on Techno2D and STG2D for long notes.");
        _longNotesAdvancedCustomScoringEnabled = _prefsCategory.CreateEntry("AdvLongNoteScoring", true,
            "Update score display for long notes every combo tick", "Updates the score display on Techno2D and STG2D for long notes every combo tick. If disabled, the score is updated on long note release.");

        if (!MountainRemovalEnabled && !LongNotesCustomScoring)
        {
            LoggerInstance.Warning("No features of the mod enabled, it can be unistalled.");
        }

        if (LongNotesAdvancedCustomScoring)
        {
            KnockingCounter = new();
        }
    }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        base.OnSceneWasLoaded(buildIndex, sceneName);
        
        KnockingCounter?.Reset();
    }
}
