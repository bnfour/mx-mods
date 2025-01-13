using System;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.VSyncAnnihilator;

public class VSyncAnnihilatorMod : MelonMod
{

    public bool ApproveChange { get; private set; }

    private MelonPreferences_Category _prefsCategory;
    private MelonPreferences_Entry<int> _prefsEntry;

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        _prefsCategory = MelonPreferences.CreateCategory("Bnfour_VSyncAnnihilator");
        _prefsEntry = _prefsCategory.CreateEntry("TargetFramerate", 240,
            "Target framerate override", "Frame limit not related to vSync. 0 for no limit -- the game will run as fast as it can, may break.");

        if (_prefsEntry.Value > 0)
        {
            Sudo(() => Application.targetFrameRate = _prefsEntry.Value);
        }

        if (QualitySettings.vSyncCount != 0)
        {
            LoggerInstance.Warning("Settings preset has vSync on! Did you patch a preset and select it?");
            LoggerInstance.Warning("Disabling now, can cause lags.");
            Sudo(() => QualitySettings.vSyncCount = 0);
        }
    }

    /// <summary>
    /// Performs the action with a guard patch disabled.
    /// </summary>
    /// <param name="action">Action to perform, should be either
    /// Application.targetFrameRate or QualitySettings.vSyncCount change.</param>
    private void Sudo(Action action)
    {
        ApproveChange = true;
        action.Invoke();
        ApproveChange = false;
    }
}
