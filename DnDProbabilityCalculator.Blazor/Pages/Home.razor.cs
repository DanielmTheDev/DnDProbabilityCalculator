using System.Text.Json;
using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.Pages;

public partial class Home
{
    [Inject]
    private ITableContextFactory TableContextFactory { get; set; } = null!;
    private IEnumerable<TableContext> _tableContexts = new List<TableContext>();

    protected override void OnInitialized()
    {
        int[] dcs = { 5, 6, 7 };
        int[] attackModifiers = { 5, 6, 7 };
        int[] armorClasses = { 5, 6, 7 };
        const int numberOfAttacks = 2;
        var inputVariables = new InputVariables(dcs, attackModifiers, armorClasses, numberOfAttacks, AdvantageType.None);
        _tableContexts =  TableContextFactory.Create(inputVariables);
    }
}