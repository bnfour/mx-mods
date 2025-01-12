using MelonLoader;

namespace Bnfour.MusynxMods.MenuTweaks;

public class MenuTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _muteEnabled;

    internal bool MenuMuteEnabled => _muteEnabled.Value;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_MenuTweaks");
        _muteEnabled = _prefsCategory.CreateEntry("Mute", true,
            "Menu mute", "Mutes menu sounds, leaving only song preview.");
    }
}
