using MelonLoader;

namespace Bnfour.MusynxMods.MenuTweaks;

public class MenuTweaksMod : MelonMod
{
    private MelonPreferences_Category _prefsCategory;

    internal bool OrdinalFixEnabled => true;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_MenuTweaks");
    }
}
