using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.OptionalOptions;

public class OptionalOptionsMod : MelonMod
{
    private bool _leftShiftDown;
    private bool _rightShiftDown;

    /// <summary>
    /// Used to track whether the UI scene is currently active.
    /// </summary>
    private bool _uiActive;

    public bool ShiftDown => _leftShiftDown || _rightShiftDown;

    /// <summary>
    /// When true (by default), custom behavior of skipping the options screen is enabled.
    /// </summary>
    public bool SkippingOptions { get; set; } = true;

    /// <summary>
    /// Checks whether the scene to work in was loaded.
    /// Also resets internal status on scene changes.
    /// </summary>
    /// <param name="buildIndex">Unused here.</param>
    /// <param name="sceneName">Scene name.</param>
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        base.OnSceneWasLoaded(buildIndex, sceneName);

        _uiActive = sceneName == "SelectSongScene" || sceneName == "SmallSelectSongScene";

        SkippingOptions = true;

        _leftShiftDown = false;
        _rightShiftDown = false;
    }

    /// <summary>
    /// If the UI scene is loaded, keeps track of shift keys' state.
    /// </summary>
    public override void OnLateUpdate()
    {
        base.OnLateUpdate();

        if (!_uiActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_leftShiftDown)
        {
            _leftShiftDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && _leftShiftDown)
        {
            _leftShiftDown = false;
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && !_rightShiftDown)
        {
            _rightShiftDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightShift) && _rightShiftDown)
        {
            _rightShiftDown = false;
        }
    }
}
