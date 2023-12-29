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
            .Add(Code.ArrowDown, IncreaseAttacks, new() { Description = "Decrease number of attacks by 1" });

        _inputVariables = InputVariables.CreateDefaultInputVariables();
        _tableContexts = TableContextFactory.Create(_inputVariables);
    }

    private void ToggleTheme()
        => Mode = Mode == DesignThemeModes.Dark ? DesignThemeModes.Light : DesignThemeModes.Dark;

    private void EnableAdvantage()
    {
        _inputVariables = _inputVariables.WithAdvantage();
        _tableContexts = TableContextFactory.Create(_inputVariables);
        StateHasChanged();
    }

    private void EnableDisadvantage()
    {
        _inputVariables = _inputVariables.WithDisadvantage();
        _tableContexts = TableContextFactory.Create(_inputVariables);
        StateHasChanged();
    }

    private void EnableNoAdvantage()
    {
        _inputVariables = _inputVariables.WithNoAdvantage();
        _tableContexts = TableContextFactory.Create(_inputVariables);
        StateHasChanged();
    }

    private void DecreaseAttacks()
    {
        _inputVariables = _inputVariables.WithDecrementedNumberOfAttacks();
        _tableContexts = TableContextFactory.Create(_inputVariables);
        StateHasChanged();
    }

    private void IncreaseAttacks()
    {
        _inputVariables = _inputVariables.WithIncrementedNumberOfAttacks();
        _tableContexts = TableContextFactory.Create(_inputVariables);
        StateHasChanged();
    }

    private void IncreaseParamters()
    {
        _inputVariables = _inputVariables.WithIncrementedColumns();
        _tableContexts = TableContextFactory.Create(_inputVariables);
        StateHasChanged();
    }

    private void DecreaseParamters()
    {
        _inputVariables = _inputVariables.WithDecrementedColumns();
        _tableContexts = TableContextFactory.Create(_inputVariables);
        StateHasChanged();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        HotKeysContext?.Dispose();
    }
}