using DnDProbabilityCalculator.Application.Table.DeliverHit;
using DnDProbabilityCalculator.Application.Table.GeneralInfo;
using DnDProbabilityCalculator.Application.Table.ReceiveHit;
using DnDProbabilityCalculator.Application.Table.SavingThrow;
using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public record TableContext
{
    private TableContext()
    {
    }

    public required GeneralTableInfo GeneralTableInfo { get; set; }
    public required SavingThrowTable SavingThrowTable { get; init; }
    public required ReceiveHitTable ReceiveHitTable { get; init; }
    public required DeliverHitTable DeliverHitTable { get; init; }

    public static TableContext FromActor(Actor actor, InputVariables inputVariables)
        => new()
        {
            GeneralTableInfo = GeneralTableInfo.FromActor(actor, inputVariables),
            SavingThrowTable = SavingThrowTable.FromActor(actor, inputVariables),
            ReceiveHitTable = ReceiveHitTable.FromActor(actor, inputVariables),
            DeliverHitTable = DeliverHitTable.FromActor(actor, inputVariables)
        };
}