using UnityEngine;

namespace Bnfour.MusynxMods.QuickQuit.Utilities;

/// <summary>
/// A class that behaves mostly like the original InputManager for keyboard input
/// -- it just defines extra input groups for the new hotkeys.
/// It should be hooked to be initialized and updated whenever the original does.
/// It's also supposed to be a singleton, this is achieved by having a single instance
/// in the mod class to be accessed from the patches.
/// </summary>
internal class ExtraInputManager
{
    private InputBlendGroup _restartGroup;
    private InputBlendGroup _quitGroup;

    // just in case, report no keypresses when uninitialized
    public bool RestartDown => _restartGroup?.FinalStatus.down ?? false;
    public bool QuitDown => _quitGroup?.FinalStatus.down ?? false;

    /// <summary>
    /// Sets up the groups, should be called with InputManager.Init
    /// that does the same (and more, not really relevant for a keyboard-only mod)
    /// </summary>
    public void Init()
    {
        _restartGroup = new InputBlendGroup
        (
            new InputSetting
            {
                type = ButtonType.keyCode,
                code = KeyCode.Backspace,
                device = InputDeviceType.keyboard
            }
        );
        _quitGroup = new InputBlendGroup
        (
            new InputSetting
            {
                type = ButtonType.keyCode,
                code = KeyCode.Delete,
                device = InputDeviceType.keyboard
            }
        );
    }

    // should be called with InputManager.Update
    public void Update()
    {
        _restartGroup.UpdateAndBlend();
        _quitGroup.UpdateAndBlend();
    }
}
