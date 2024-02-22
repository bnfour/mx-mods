using System;
using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.VSyncAnnihilator;

public class VSyncAnnihilatorMod : MelonMod
{

    public bool ApproveChange { get; private set; }

    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();
        // TODO make framerate configurable
        Sudo(() => Application.targetFrameRate = 240);

        if (QualitySettings.vSyncCount != 0)
        {
            LoggerInstance.Warning("Selected settings preset has vSync on! Did you patch a preset and choose it?");
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
