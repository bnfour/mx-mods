using MelonLoader;

namespace Bnfour.MusynxMods.Experimental;

public class ExperimentalMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        base.OnInitializeMelon();

        LoggerInstance.Msg("Experimental mod online");
    }
}
