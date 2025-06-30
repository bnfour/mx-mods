using MelonLoader;

using Bnfour.MusynxMods.SongInfo.Utilities;

namespace Bnfour.MusynxMods.SongInfo;

public class SongInfoMod : MelonMod
{
    internal readonly SongDataProvider songDataProvider = new();
}
