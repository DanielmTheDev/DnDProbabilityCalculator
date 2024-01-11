using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor.HotKeys2;

namespace DnDProbabilityCalculator.Blazor.Components;

public partial class HotkeyHelp
{
    [Parameter]
    public List<HotKeyEntry> Content { get; set; } = [];

    protected override void OnInitialized()
    {
        Console.Out.Write(Content);
    }
}