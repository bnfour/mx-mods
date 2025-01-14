using MelonLoader;

namespace Bnfour.MusynxMods.MenuTweaks;

public class MenuTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _ordinalsFixEnabled;
    private MelonPreferences_Entry<bool> _muteEnabled;

    internal bool OrdinalFixEnabled => _ordinalsFixEnabled.Value;
    internal bool MenuMuteEnabled => _muteEnabled.Value;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_MenuTweaks");
        _ordinalsFixEnabled = _prefsCategory.CreateEntry("OrdinalsFix", true,
            "Ordinals fix", "Fixes suffixes for your rank in song selection menus.");
        _muteEnabled = _prefsCategory.CreateEntry("Mute", true,
            "Menu mute", "Mutes menu sounds, leaving only song preview.");

        if (!OrdinalFixEnabled && !MenuMuteEnabled)
        {
            LoggerInstance.Warning("No features of the mod enabled, it can be unistalled.");
        }
    }
}
