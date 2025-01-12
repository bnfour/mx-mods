using MelonLoader;

namespace Bnfour.MusynxMods.MenuTweaks;

public class MenuTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _ordinalsFixEnabled;

    internal bool OrdinalFixEnabled => _ordinalsFixEnabled.Value;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_MenuTweaks");
        _ordinalsFixEnabled = _prefsCategory.CreateEntry("OrdinalsFix", true,
            "Ordinals fix", "Fixes suffixes for your rank in song selection menus.");
    }
}
