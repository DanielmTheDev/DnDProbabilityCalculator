using System.Text.Json;
using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Blazor.Application;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.Pages;

public partial class Home
{
    [Inject]
    private ITableContextProvider TableContextProvider { get; set; } = null!;
    private IEnumerable<TableContext> _tableContexts = new List<TableContext>();
    private string PartyAsJson => JsonSerializer.Serialize(_tableContexts, new JsonSerializerOptions
    {
        WriteIndented = true
    });

    protected override void OnInitialized()
        => _tableContexts = TableContextProvider.Get();
}