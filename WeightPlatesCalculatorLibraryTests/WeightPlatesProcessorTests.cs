using WeightPlatesCalculatorLibrary.Processors;

namespace WeightPlatesCalculatorLibraryTests;

public class WeightPlatesProcessorTests
{
    public static IEnumerable<object[]> When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest_Data =>
        new List<object[]>
        {
            new object[] { new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 2.5 }, new List<double> { 2.5 }, 2.5 },
            new object[] { new List<double> { 2.5, 2.5, 2.5, 2.5, 10 }, new List<double> { 10 }, 10 },
            new object[] { new List<double> { 0.5, 1.25, 1.25, 2.5 }, new List<double> { 0.5, 2.5 }, 3 }
        };

    public static IEnumerable<object[]> When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination_Data =>
    new List<object[]>
    {
            new object[] { new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 2.5 }, new List<double> { }, 1.5, 2 },
            new object[] { new List<double> { 2.5, 2.5, 2.5, 2.5 }, new List<double> { }, 10, 3 },
            new object[] { new List<double> { 1.25, 2.5, 10, 10, 10 }, new List<double> { }, 33.75, 4 }
    };

    public static IEnumerable<object[]> When_TargetWeight_Not_Divisible_By_0Point25_With_Remainder_0_GetPlatesForTargetWeight_Should_Throw_Exception_Data =>
    new List<object[]>
    {
            new object[] { new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 2.5 }, 2.51 },
            new object[] { new List<double> { 2.5, 2.5, 2.5, 2.5, 10 }, 10.49 },
            new object[] { new List<double> { 0.5, 1.25, 1.25, 2.5 }, 3.09 },
            new object[] { new List<double> { 0.5, 1.25, 1.25, 2.5 }, 0.99999 },
            new object[] { new List<double> { 0.5, 1.25, 1.25, 2.5 }, 0.00001 },
    };

    [Theory]
    [InlineData(0.5, 0.5, 0.5)]
    [InlineData(1.25, 1.25, 1.25)]
    [InlineData(2.5, 2.5, 2.5)]
    [InlineData(10, 10, 10)]
    public void When_Matching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_Single_Weight(double weight, double expectedWeight, double targetWeight)
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
                                                        10,
                                                        targetWeight,
                                                        result);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0.5, 1.25)]
    [InlineData(1.25, 0.5)]
    [InlineData(1.25, 10)]
    public void When_Mismatching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_No_Weights(double weight, double targetWeight)
    {
        // Arrange
        List<double> expected = new();

        List<double> result = new();

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(new List<double> { weight },
                                                        10,
                                                        targetWeight,
                                                        result);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest_Data))]
    public void When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest(List<double> weights, List<double> expected, double targetWeight)
    {
        // Arrange
        List<double> result = new();

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(weights,
                                                        10,
                                                        targetWeight,
                                                        result);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination_Data))]
    public void When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination(List<double> weights, List<double> expected, double targetWeight, int maxPlates)
    {
        // Arrange
        List<double> result = new();

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(weights,
                                                        maxPlates,
                                                        targetWeight,
                                                        result);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(When_TargetWeight_Not_Divisible_By_0Point25_With_Remainder_0_GetPlatesForTargetWeight_Should_Throw_Exception_Data))]
    public void When_TargetWeight_Not_Divisible_By_0Point25_With_Remainder_0_GetPlatesForTargetWeight_Should_Throw_Exception(List<double> weights,  double targetWeight)
    {
        // Arrange
        List<double> result = new();

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act and Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => weightPlatesCalculator.GetPlatesForTargetWeight(weights,
                                                        10,
                                                        targetWeight,
                                                        result));
    }
}
