using MelonLoader;

namespace Bnfour.MusynxMods.SkinTweaks.Utilities;

public class KnockingCounter
{
    private int _count;
    // TODO remove, reference as usual to report broken values
    private readonly MelonLogger.Instance _logger;

    public KnockingCounter(MelonLogger.Instance logger)
    {
        _logger = logger;
    }

    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            _logger.Msg($"knocking count: {_count}{((_count < 0 || _count > 4) ? " AAAAA" : "")}");
        }
    }

    public void Reset() => _count = 0;
}
