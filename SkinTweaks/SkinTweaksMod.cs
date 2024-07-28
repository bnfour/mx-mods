using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks;

public class SkinTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _mountainRemovalEnabled;

    internal bool MountainRemovalEnabled => _mountainRemovalEnabled.Value;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_SkinTweaks");

        _mountainRemovalEnabled = _prefsCategory.CreateEntry("MountainRemoval", true,
            "Mountain removal", "Removes moving white mountain overlays from UI0D.");
    }
}
