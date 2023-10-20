using Nautilus.Options.Attributes;
using Nautilus.Json;

using UnityEngine;

// https://github.com/RamuneNeptune/SubnauticaMods.Nautilus/blob/main/SubnauticaMods/PrawnSuitLightSwitch/Config.cs
namespace VS.Subnautica.CyclopsCameraZoom
{
    [Menu("Cyclops Camera Zoom")]
    public class Config : ConfigFile
    {
        [Keybind("Zooms in and out.")]
        public KeyCode toggle = KeyCode.Space;
    }
}
