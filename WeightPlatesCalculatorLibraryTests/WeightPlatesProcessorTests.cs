using WeightPlatesCalculatorLibrary.Processors;

namespace WeightPlatesCalculatorLibraryTests;

public class WeightPlatesProcessorTests
{
    [Theory]
    [InlineData(0.5, 0.5, 0.5)]
    [InlineData(1.25, 1.25, 1.25)]
    [InlineData(2.5, 2.5, 2.5)]
    [InlineData(10, 10, 10)]
    public void MatchingTargetAndSingleWeightShouldReturnSingleWeight(double weight, double expectedWeight, double targetWeight)
    {
        // Arrange
        List<double> expected = new()
        {
            expectedWeight
        };

        List<double> result = new();

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(new List<double> { weight },
                                                        1,
                                                        targetWeight,
                                                        result);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0.5, 1.25)]
    [InlineData(1.25, 0.5)]
    [InlineData(1.25, 10)]
    public void MismatchingTargetAndSingleWeightShouldReturnNoWeights(double weight, double targetWeight)
    {
        // Arrange
        List<double> expected = new();

        List<double> result = new();

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(new List<double> { weight },
                                                        1,
                                                        targetWeight,
                                                        result);

        // Assert
        Assert.Equal(expected, result);
    }
}
