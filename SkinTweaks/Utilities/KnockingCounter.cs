using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks.Utilities;

/// <summary>
/// A helper to keep track whether there are active long notes present for advanced scoring.
/// Can be null -- if so, the advances scoring is disabled.
/// </summary>
public class KnockingCounter
{
    private int _count;

    public void StartKnock()
    {
        _count++;
    }

    public void EndKnock()
    {
        _count--;

        // TODO discriminate between 4k and 6k modes?
        if (_count < 0 || _count > 6)
        {
            Melon<SkinTweaksMod>.Logger.Error("Something went terribly wrong with long note counter. Please restart the level.");
        }
    }

    public void Reset() => _count = 0;

    public bool AnyLongNotes => _count > 0;
}
