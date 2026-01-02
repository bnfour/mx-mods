using MelonLoader;

namespace Bnfour.MusynxMods.PlentifulStats.Utilities;

/// <summary>
/// A simple helper to store a value from the stack for later use.
/// The sole reason for it being a static class is that it's easiest to call.
/// </summary>
internal static class SyncNumberHelper
{
    /// <summary>
    /// Stores the provided value (allegedly the previous high score) in the mod instance
    /// because the original value might be overwritten later.
    /// </summary>
    /// <param name="value">Value to store in the mod instance for later use.</param>
    /// <returns>The same value, so it's on top of the stack like nothing happened.</returns>
    internal static int Store(int value)
    {
        Melon<PlentifulStatsMod>.Instance.SyncNumber = value;

        return value;
    }
}
