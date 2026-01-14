using MelonLoader;

using Bnfour.MusynxMods.MenuTweaks.Data;
using Bnfour.MusynxMods.MenuTweaks.Utilities;

namespace Bnfour.MusynxMods.MenuTweaks;

public class MenuTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<bool> _ordinalsFixEnabled;
    private MelonPreferences_Entry<bool> _muteEnabled;
    private MelonPreferences_Entry<bool> _shadowNormalizationEnabled;
    private MelonPreferences_Entry<TextShadowNormalization> _shadowNormalizationMethod;
    private MelonPreferences_Entry<bool> _noMissingInfWarpsEnabled;

    internal bool OrdinalFixEnabled => _ordinalsFixEnabled.Value;
    internal bool MenuMuteEnabled => _muteEnabled.Value;
    internal bool ShadowNormalizationEnabled => _shadowNormalizationEnabled.Value;
    internal TextShadowNormalization ShadowNormalizationMethod => _shadowNormalizationMethod.Value;
    internal bool NoWarpsWhenInfMissingEnabled => _noMissingInfWarpsEnabled.Value;

    internal ShadowColorHelper shadowColorHelper;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_MenuTweaks");
        _ordinalsFixEnabled = _prefsCategory.CreateEntry("OrdinalsFix", true,
            "Ordinals fix", "Fixes suffixes for your rank in song selection menus.");
        _muteEnabled = _prefsCategory.CreateEntry("Mute", true,
            "Menu mute", "Mutes menu sounds, leaving only song preview.");
        _shadowNormalizationEnabled = _prefsCategory.CreateEntry("ShadowsFix", true,
            "Uniform shadow colors", "Makes text shadows in the big menu the same color.");
        _shadowNormalizationMethod = _prefsCategory.CreateEntry("ShadowsFixMethod", TextShadowNormalization.Lighten,
            "Uniform shadow color", "What to do with the text shadows -- lighten the darker ones or darken the lighter ones to match the others.");
        _noMissingInfWarpsEnabled = _prefsCategory.CreateEntry("NoInfernoWarping", true,
            "No song switch on lack of Inferno difficulty in list menu", "Prevents difficulty switch also changing the song when the selected song has no Inferno difficulty for the list (small) menu");

        if (ShadowNormalizationEnabled)
        {
            shadowColorHelper = new(ShadowNormalizationMethod);
        }

        if (!OrdinalFixEnabled && !MenuMuteEnabled && !ShadowNormalizationEnabled
            && !NoWarpsWhenInfMissingEnabled)
        {
            LoggerInstance.Warning("No features of the mod enabled, it can be uninstalled.");
        }
    }
}
