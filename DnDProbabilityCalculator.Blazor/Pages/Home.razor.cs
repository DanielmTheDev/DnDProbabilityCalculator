using DnDProbabilityCalculator.Application.Table;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Toolbelt.Blazor.HotKeys2;

namespace DnDProbabilityCalculator.Blazor.Pages;

public partial class Home : IDisposable
{
    [Inject]
    private ITableContextFactory TableContextFactory { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private HotKeys HotKeys { get; set; } = null!;

    private HotKeysContext? HotKeysContext { get; set; }
    private DesignThemeModes Mode { get; set; } = DesignThemeModes.Dark;
    private IEnumerable<TableContext> _tableContexts = new List<TableContext>();
    private InputVariables _inputVariables = null!;

    protected override void OnInitialized()
    {
        HotKeysContext = HotKeys.CreateContext()
            .Add(Code.A, EnableAdvantage, new() { Description = "Enable advantage" })
            .Add(Code.D, EnableDisadvantage, new() { Description = "Enable Disadvantage" })
            .Add(Code.S, EnableNoAdvantage, new() { Description = "EnableNoAdvantage" })
            .Add(Code.ArrowRight, IncreaseParamters, new() { Description = "Increase all input parameters (DCs, ACs, and Modifiers) by 1" })
            .Add(Code.ArrowLeft, DecreaseParamters, new() { Description = "Decrease all input parameters (DCs, ACs, and Modifiers) by 1" })
            .Add(Code.ArrowUp, DecreaseAttacks, new() { Description = "Increase number of attacks by 1" })
            .Add(Code.ArrowDown, IncreaseAttacks, new() { Description = "Decrease number of attacks by 1" })
            .Add(Key.Question, async () => await ToggleShowHelp(), new() { Description = "Show this help screen" });

        _inputVariables = InputVariables.CreateDefaultInputVariables();
        _tableContexts = TableContextFactory.Create(_inputVariables);
    }

    private async Task ToggleShowHelp()
    {
        DialogParameters parameters = new()
        {
            Title = $"Hello Daniel",
            PrimaryAction = "Yes",
            PrimaryActionEnabled = false,
            SecondaryAction = "No",
            Width = "500px",
            TrapFocus = true,
            Modal = true,
            PreventScroll = true
        };

        var x = await DialogService.ShowDialogAsync<HotkeyHelp>(parameters);
    }

    private void UpdateTable(Func<InputVariables> updateFunction)
    {
        _inputVariables = updateFunction();
        _tableContexts = TableContextFactory.Create(_inputVariables);
        StateHasChanged();
    }

    private void ToggleTheme()
        => Mode = Mode == DesignThemeModes.Dark ? DesignThemeModes.Light : DesignThemeModes.Dark;

    private void EnableAdvantage()
        => UpdateTable(_inputVariables.WithAdvantage);

    private void EnableDisadvantage()
        => UpdateTable(_inputVariables.WithDisadvantage);

    private void EnableNoAdvantage()
        => UpdateTable(_inputVariables.WithNoAdvantage);

    private void DecreaseAttacks()
        => UpdateTable(_inputVariables.WithDecrementedNumberOfAttacks);

    private void IncreaseAttacks()
        => UpdateTable(_inputVariables.WithIncrementedNumberOfAttacks);

    private void IncreaseParamters()
        => UpdateTable(_inputVariables.WithIncrementedColumns);

    private void DecreaseParamters()
        => UpdateTable(_inputVariables.WithDecrementedColumns);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        HotKeysContext?.Dispose();
    }

    private static string GetColorClass(double cell)
        => cell switch
        {
            < 0.25 => "red",
            < 0.45 => "orange",
            < 0.65 => "yellow",
            < 0.85 => "turquoise",
            _ => "green",
        };

    private static string GetInvertedColorClass(double cell)
        => cell switch
        {
            < 0.25 => "green",
            < 0.45 => "turquoise",
            < 0.65 => "yellow",
            < 0.85 => "orange",
            _ => "red",
        };
}