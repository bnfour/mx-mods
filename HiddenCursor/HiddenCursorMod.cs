using MelonLoader;
using UnityEngine;

namespace Bnfour.MusynxMods.HiddenCursor;

public class HiddenCursorMod : MelonMod
{
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        base.OnSceneWasLoaded(buildIndex, sceneName);

        var isGameScene = sceneName.StartsWith("UI") && sceneName.EndsWith("_PlayingScene");
        Cursor.visible = !isGameScene;
    }
}
