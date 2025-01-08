using HarmonyLib;
using MelonLoader;
using UnityEngine;
using UnityEngine.Rendering;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch(typeof(UI0E_BgController), nameof(UI0E_BgController.GetSongBgSprite))]
public class UI0E_BgControllerGetSongBgSpritePatch
{
    private static void Prefix(UI0E_BgController __instance)
    {
        // Melon<SkinTweaksMod>.Logger.Msg("GetSongBgSprite called");
        __instance.bg.gameObject.SetActive(true);
        __instance.bg.gameObject.layer = 13;
        __instance.bg.color = new Color(1f, 1f, 1f, 0.33f);

        // Melon<SkinTweaksMod>.Logger.Msg($"bg layer {__instance.bg.gameObject.layer}");
        // Melon<SkinTweaksMod>.Logger.Msg($"plane layer {__instance.fightPlane.layer}");

        // foreach (var cam in Camera.allCameras)
        // {
        //     var sr = cam.GetComponentsInChildren<SpriteRenderer>();
        //     Melon<SkinTweaksMod>.Logger.Msg($"cam {cam.name}");
        //     Melon<SkinTweaksMod>.Logger.Msg($"\tortho {cam.orthographic}");
        //     Melon<SkinTweaksMod>.Logger.Msg($"\tculling mask {cam.cullingMask}");
        //     Melon<SkinTweaksMod>.Logger.Msg($"\tclear flags {cam.clearFlags.ToString()}");
        //     Melon<SkinTweaksMod>.Logger.Msg($"\tbg color {cam.backgroundColor.ToString()}");
        // }

        var customCameraHolder = GameObject.Instantiate(__instance.bgCamera, __instance.bgCamera.transform.parent);
        customCameraHolder.name = "bnTertiaryCamHolder";
        Melon<SkinTweaksMod>.Logger.Msg($"enabled {customCameraHolder.activeSelf}");

        var backgroundBackgroundCamera = customCameraHolder.GetComponent<Camera>();
        backgroundBackgroundCamera.CopyFrom(Camera.main);

        backgroundBackgroundCamera.name = "Background background camera (things are complicated)";
        backgroundBackgroundCamera.tag = "bnThirdCamForBg";
        backgroundBackgroundCamera.depth = -2;
        backgroundBackgroundCamera.cullingMask = 1 << 13;
        backgroundBackgroundCamera.clearFlags = CameraClearFlags.SolidColor;
        backgroundBackgroundCamera.backgroundColor = Color.black;//new Color(0.792f, 0.314f, 0.063f, 1f);
        backgroundBackgroundCamera.enabled = true;

        var cameraz = backgroundBackgroundCamera.transform.position.z;
        var spritez = __instance.bg.transform.position.z;

        Melon<SkinTweaksMod>.Logger.Msg($"cam {cameraz}, spr {spritez}");

        var spaceshipsCamera = __instance.bgCamera.GetComponent<Camera>();
        spaceshipsCamera.clearFlags = CameraClearFlags.Depth;
        spaceshipsCamera.backgroundColor = Color.clear;

        // Melon<SkinTweaksMod>.Logger.Msg($"order {__instance.fightPlaneController.leftPlayer.planeSprite.sortingOrder}, layer id {__instance.fightPlaneController.leftPlayer.planeSprite.sortingLayerID}, name {__instance.fightPlaneController.leftPlayer.planeSprite.sortingLayerName}");
        
    }

    private static void Postfix(UI0E_BgController __instance)
    {
        Melon<SkinTweaksMod>.Logger.Msg("GetSongBgSprite finished");
        foreach (var cam in Camera.allCameras)
        {
            var sr = cam.GetComponentsInChildren<SpriteRenderer>();
            Melon<SkinTweaksMod>.Logger.Msg($"cam {cam.name}");
            Melon<SkinTweaksMod>.Logger.Msg($"\tortho {cam.orthographic}");
            Melon<SkinTweaksMod>.Logger.Msg($"\tculling mask {cam.cullingMask}");
            Melon<SkinTweaksMod>.Logger.Msg($"\tclear flags {cam.clearFlags.ToString()}");
            Melon<SkinTweaksMod>.Logger.Msg($"\tbg color {cam.backgroundColor.ToString()}");
            Melon<SkinTweaksMod>.Logger.Msg($"\tdepth {cam.depth}");
        }
        // Melon<SkinTweaksMod>.Logger.Msg($"order {__instance.bg.sortingOrder}, layer id {__instance.bg.sortingLayerID}, name {__instance.bg.sortingLayerName}");
        // Melon<SkinTweaksMod>.Logger.Msg($"sprite {__instance.bg.sprite.name}");
        // Melon<SkinTweaksMod>.Logger.Msg($"tex {__instance.bg.sprite.texture.name}");
    }
}
