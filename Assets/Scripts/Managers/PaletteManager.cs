using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PaletteManager
{
    public static void SetSpritePalette(Material m, ColourPalette cp)
    {
        m.SetColor("hc", cp.hightlightColour);
        m.SetColor("shc", cp.secondaryHightlightColour);
        m.SetColor("mrc", cp.midrangeColour);
        m.SetColor("msc", cp.midShadowColour);
        m.SetColor("sc", cp.shadowColour);
        m.SetColor("tc", cp.tintColour);
    }
}
