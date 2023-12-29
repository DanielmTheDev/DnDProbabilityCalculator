using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor.HotKeys2;

namespace DnDProbabilityCalculator.Blazor.Pages;

public partial class Home : IDisposable
{
    [Inject]
    private ITableContextFactory TableContextFactory { get; set; } = null!;

    [Inject]
    private HotKeys HotKeys { get; set; } = null!;

    private HotKeysContext? HotKeysContext { get; set; }
    private IEnumerable<TableContext> _tableContexts = new List<TableContext>();

    protected override void OnInitialized()
    {
        HotKeysContext = HotKeys.CreateContext()
            .Add(Code.ArrowRight, IncreaseParamters, new() { Description = "Increase all input parameters (DCs, ACs, and Modifiers) by 1" })
            .Add(Code.ArrowLeft, DecreaseParamters, new() { Description = "Decrease all input parameters (DCs, ACs, and Modifiers) by 1" })
            .Add(Code.ArrowUp, IncreaseAttacks, new() { Description = "Increase number of attacks by 1" })
            .Add(Code.ArrowDown, DecreaseAttacks, new() { Description = "Decrease number of attacks by 1" });

        int[] dcs = { 5, 6, 7 };
        int[] attackModifiers = { 5, 6, 7 };
        int[] armorClasses = { 5, 6, 7 };
        const int numberOfAttacks = 2;
        var inputVariables = new InputVariables(dcs, attackModifiers, armorClasses, numberOfAttacks, AdvantageType.None);
        _tableContexts = TableContextFactory.Create(inputVariables);
    }

    private void DecreaseAttacks()
    {
        throw new NotImplementedException();
    }

    private void IncreaseAttacks()
    {
        throw new NotImplementedException();
    }

    private void IncreaseParamters(HotKeyEntryByCode obj)
    {
        Console.Out.WriteLine(obj.Code);
    }

    private void DecreaseParamters(HotKeyEntryByCode obj)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        HotKeysContext?.Dispose();
    }
}