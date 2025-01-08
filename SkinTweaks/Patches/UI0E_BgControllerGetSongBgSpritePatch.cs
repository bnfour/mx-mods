using HarmonyLib;
using UnityEngine;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

/// <summary>
/// A patch that sets up rendering for background art under the space game overlay.
/// </summary>
[HarmonyPatch(typeof(UI0E_BgController), nameof(UI0E_BgController.GetSongBgSprite))]
public class UI0E_BgControllerGetSongBgSpritePatch
{
    // the main camera skips layers 11 through 16, 16 is taken by bgCamera
    private const int BgLayer = 13;

    private static void Prefix(UI0E_BgController __instance)
    {
        // TODO return early if feature not enabled

        // set up the bg sprite to display
        __instance.bg.gameObject.SetActive(true);
        __instance.bg.gameObject.layer = BgLayer;
        // TODO configurable alpha
        __instance.bg.color = new Color(1f, 1f, 1f, 0.33f);

        // clone the bgCamera object
        var customCameraHolder = GameObject.Instantiate(__instance.bgCamera, __instance.bgCamera.transform.parent);
        customCameraHolder.name = "bnThirdCamHolder";
        // take most of the settings from the main camera
        var backgroundBackgroundCamera = customCameraHolder.GetComponent<Camera>();
        backgroundBackgroundCamera.CopyFrom(Camera.main);
        backgroundBackgroundCamera.name = "Background background camera (things are complicated)";
        backgroundBackgroundCamera.tag = "bnThirdCamForBg";
        // put below the other cameras, and set to render only the bg layer
        backgroundBackgroundCamera.depth = -2;
        backgroundBackgroundCamera.cullingMask = 1 << BgLayer;
        backgroundBackgroundCamera.clearFlags = CameraClearFlags.SolidColor;
        // TODO consider making the color configurable by user
        backgroundBackgroundCamera.backgroundColor = Color.black;
        backgroundBackgroundCamera.enabled = true;

        // set up the existing second camera for transparency to show output from the third one
        var spaceshipsCamera = __instance.bgCamera.GetComponent<Camera>();
        spaceshipsCamera.clearFlags = CameraClearFlags.Depth;
        spaceshipsCamera.backgroundColor = Color.clear;
    }
}
