using System.ComponentModel;

namespace GameBox.Media;

public enum GamePlatform
{
    [Description("PC (Windows)")]
    PC,
    [Description("Nintendo 3DS")]
    N3DS,
    [Description("Nintendo 64")]
    N64,
    [Description("Nintendo Switch")]
    NSwitch,
    [Description("Nintendo Wii U")]
    NWiiU
}
