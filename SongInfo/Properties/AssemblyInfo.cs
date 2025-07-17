using System.Reflection;
using MelonLoader;

using Bnfour.MusynxMods.SongInfo;

[assembly: MelonInfo(typeof(SongInfoMod), "Song info", "1.0.0", "bnfour", "https://github.com/bnfour/mx-mods")]
[assembly: MelonGame("coweye", "MUSYNX")]
[assembly: MelonColor(255, 202, 80, 16)]
[assembly: MelonAuthorColor(255, 128, 128, 128)]

[assembly: AssemblyDescription("Shows song's duration and BPM in selection menus")]
[assembly: AssemblyCopyright("bnfour 2025; open-source")]
