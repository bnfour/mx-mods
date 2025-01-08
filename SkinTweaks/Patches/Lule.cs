using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Bnfour.MusynxMods.SkinTweaks.Patches;

[HarmonyPatch(typeof(UI0E_SongPlayer), "Awake")]
public class Lule
{
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
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
