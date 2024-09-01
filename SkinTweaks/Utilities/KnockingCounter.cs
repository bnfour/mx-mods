using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks.Utilities;

public class KnockingCounter
{
    private int _count;

    // TODO is a locker on there necessary?
    public void StartKnock()
    {
        _count++;
    }

    public void EndKnock()
    {
        _count--;
    }

    public void Reset() => _count = 0;

    public bool AnyLongNotes => _count > 0;
}
