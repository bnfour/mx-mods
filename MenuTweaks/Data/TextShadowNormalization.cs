namespace Bnfour.MusynxMods.MenuTweaks.Data;

/// <summary>
/// Ways to make dark text shadows uniform in color in the big menu screen.
/// </summary>
public enum TextShadowNormalization
{
    /// <summary>
    /// Turn pure black shadows into gray. The default.
    /// </summary>
    Lighten = 0,
    /// <summary>
    /// Turn gray shadows into pure black.
    /// </summary>
    Darken
}
