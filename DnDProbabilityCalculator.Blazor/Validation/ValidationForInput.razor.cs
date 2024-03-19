using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.Validation;

public partial class ValidationForInput
{

    [Parameter]
    public Expression<Func<object?>>? For { get; set; }
}