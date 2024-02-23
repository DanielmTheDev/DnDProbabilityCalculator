using DnDProbabilityCalculator.Blazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Toolbelt.Blazor.HotKeys2;

namespace DnDProbabilityCalculator.Blazor.Home;

public partial class ButtonBar
{
    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Parameter]
    public List<HotKeyEntry> HotKeys { get; set; } = [];

    private DesignThemeModes Mode { get; set; } = DesignThemeModes.Dark;

    private void ToggleTheme()
        => Mode = Mode == DesignThemeModes.Dark ? DesignThemeModes.Light : DesignThemeModes.Dark;

    public async Task ShowHelpDialog()
    {
        var parameters = new DialogParameters
        {
            Title = "Controls",
            Width = "500px",
            PrimaryAction = string.Empty,
            SecondaryAction = string.Empty,
            TrapFocus = true,
            PreventScroll = true
        };

        await DialogService.ShowDialogAsync<HotkeyHelp>(HotKeys, parameters);
    }
}