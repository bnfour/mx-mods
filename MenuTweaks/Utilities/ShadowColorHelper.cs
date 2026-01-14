using System;

using UnityEngine;
using UnityEngine.UI;

using Bnfour.MusynxMods.MenuTweaks.Data;

namespace Bnfour.MusynxMods.MenuTweaks.Utilities;

internal class ShadowColorHelper
{
    /// <summary>
    /// For float comparison of color components, if difference is less than that,
    /// we consider the values to be equal.
    /// </summary>
    private const float Epsilon = 0.001f;
    /// <summary>
    /// The gray color used alongside pure black in vanilla.
    /// </summary>
    private static readonly Color LighterShadow = new(0.235f, 0.235f, 0.235f, 1.000f);

    private readonly Color _shadowToKeep;
    private readonly Color _shadowToChange;

    internal ShadowColorHelper(TextShadowNormalization method)
    {
        _shadowToKeep = method switch
        {
            TextShadowNormalization.Lighten => LighterShadow,
            TextShadowNormalization.Darken => Color.black,
            _ => throw new ApplicationException("Unknown shadow normalization method")
        };

        _shadowToChange = method switch
        {
            TextShadowNormalization.Lighten => Color.black,
            TextShadowNormalization.Darken => LighterShadow,
            _ => throw new ApplicationException("Unknown shadow normalization method")
        };
    }

    internal void Process(Text text)
    {
        if (CloseEnough(text.color, _shadowToChange))
        {
            text.color = _shadowToKeep;
        }
    }
    
    private bool CloseEnough(Color one, Color two)
    {
        return Math.Abs(one.r - two.r) <= Epsilon
            && Math.Abs(one.g - two.g) <= Epsilon
            && Math.Abs(one.b - two.b) <= Epsilon
            && Math.Abs(one.a - two.a) <= Epsilon;
    }
}
