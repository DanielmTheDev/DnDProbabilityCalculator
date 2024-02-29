using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Blazor.PartyCreation;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor.HotKeys2;

namespace DnDProbabilityCalculator.Blazor.PartyDisplay;

public partial class ProbabilityCalculator
{
    [Inject]
    public IPartyClient PartyClient { get; set; } = null!;

    [Parameter]
    public string PartyId { get; set; } = string.Empty;

    [Inject]
    private ITableContextFactory TableContextFactory { get; set; } = null!;

    [Inject]
    private HotKeys HotKeys { get; set; } = null!;

    private ButtonBar _buttonBar = null!;
    private HotKeysContext HotKeysContext { get; set; } = null!;
    private IEnumerable<TableContext> _tableContexts = new List<TableContext>();
    private InputVariables _inputVariables = null!;
    private Party? _party;

    protected override async Task OnInitializedAsync()
    {
        var result = await PartyClient.Get(PartyId);

        if (result.IsSuccess)
        {
            _party = result.Value;
            _inputVariables = InputVariables.CreateDefaultInputVariables();
            _tableContexts = TableContextFactory.Create(_inputVariables, _party);
            InitializeHotkeys();
        }
        else
        {
            await Console.Out.WriteLineAsync(result.Errors.First().Message);
        }
    }

    private void InitializeHotkeys()
        => HotKeysContext = HotKeys.CreateContext()
            .Add(Code.A, EnableAdvantage, new() { Description = "Enable Advantage" })
            .Add(Code.D, EnableDisadvantage, new() { Description = "Enable Disadvantage" })
            .Add(Code.S, EnableNoAdvantage, new() { Description = "Disable Advantage and Disadvantage" })
            .Add(Code.ArrowRight, IncreaseParamters, new() { Description = "Increase all input parameters (DCs, ACs, and Modifiers) by 1" })
            .Add(Code.ArrowLeft, DecreaseParamters, new() { Description = "Decrease all input parameters (DCs, ACs, and Modifiers) by 1" })
            .Add(Code.ArrowUp, DecreaseAttacks, new() { Description = "Increase number of attacks by 1" })
            .Add(Code.ArrowDown, IncreaseAttacks, new() { Description = "Decrease number of attacks by 1" })
            .Add(Key.Question, async () => await _buttonBar.ShowHelpDialog(), new() { Description = "Show this help screen" });

    private void UpdateTable(Func<InputVariables> updateFunction)
    {
        _inputVariables = updateFunction();
        if (_party != null)
        {
            _tableContexts = TableContextFactory.Create(_inputVariables, _party);
        }
        StateHasChanged();
    }

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
        HotKeysContext.Dispose();
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

    private static string GetAdvantageColorClass(AdvantageType advantage)
        => advantage switch
        {
            AdvantageType.Advantage => "green-background",
            AdvantageType.Disadvantage => "red-background",
            _ => string.Empty
        };
}