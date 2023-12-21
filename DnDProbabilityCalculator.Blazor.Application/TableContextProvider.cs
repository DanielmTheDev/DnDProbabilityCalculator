using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Blazor.Application;

public class TableContextProvider(ITableContextFactory tableContextFactory) : ITableContextProvider
{
    public IEnumerable<TableContext> Get()
    {
        int[] dcs = { 5, 6, 7 };
        int[] attackModifiers = { 5, 6, 7 };
        int[] armorClasses = { 5, 6, 7 };
        const int numberOfAttacks = 2;
        var inputVariables = new InputVariables(dcs, attackModifiers, armorClasses, numberOfAttacks, AdvantageType.None);
        return tableContextFactory.Create(inputVariables);
    }
}