﻿using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.Context;

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
    {
        return new()
        {
            GeneralTableInfo = GeneralTableInfo.FromActor(actor, inputVariables),
            SavingThrowTable = SavingThrowTable.FromActor(actor, inputVariables),
            ReceiveHitTable = ReceiveHitTable.FromActor(actor, inputVariables),
            DeliverHitTable = DeliverHitTable.FromActor(actor, inputVariables)
        };
    }
}