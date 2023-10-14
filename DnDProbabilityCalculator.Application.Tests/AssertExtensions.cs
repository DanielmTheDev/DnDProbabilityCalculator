using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests;

public static class AssertExtensions
{
    public static void AssertElementsAreContainedIn(this IEnumerable<string> elementsThatAreContained, IEnumerable<string> elementsThatContain)
        => elementsThatAreContained.Zip(elementsThatContain).ToList().ForEach(tuple => Assert.IsTrue(tuple.Second.Contains(tuple.First)));

}