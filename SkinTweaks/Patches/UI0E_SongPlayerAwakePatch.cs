using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using HarmonyLib;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

/// <summary>
/// A patch that restores the background image load for STG2D (UI0E).
/// It is replaced by a pure black sprite by default, even though it's never
/// shown anyway, so it's done for performance reasons?
/// </summary>
[HarmonyPatch(typeof(UI0E_SongPlayer), "Awake")]
public class UI0E_SongPlayerAwakePatch
{
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            // when UI0E_SongPlayer.Black is about to be put to the top of the stack,
            // call UI0E_SongPlayer.songInfoCore.LoadSongBgImmediately() instead
            // and push its result to stack like other UI skins do
            if (instruction.opcode == OpCodes.Ldfld)
            {
                var operand = instruction.operand as FieldInfo;
                if (operand.DeclaringType == typeof(UI0E_SongPlayer)
                    && operand.Name == nameof(UI0E_SongPlayer.Black))
                {
                    yield return CodeInstruction.Call(typeof(UI0E_SongPlayer), "get_songInfoCore");
                    var methodInfo = AccessTools.Method(typeof(SongInfoCore), "LoadSongBgImmediately");
                    yield return new CodeInstruction(OpCodes.Callvirt, methodInfo);
                }
                else
                {
                    yield return instruction;
                }
            }
            else
            {
                yield return instruction;
            }
        }
    }
}
