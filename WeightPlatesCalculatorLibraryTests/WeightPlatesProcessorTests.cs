using System.Diagnostics;
using WeightPlatesCalculatorLibrary.Models;
using WeightPlatesCalculatorLibrary.Processors;

namespace WeightPlatesCalculatorLibraryTests;

public class WeightPlatesProcessorTests
{
    public static IEnumerable<object[]> When_Matching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_Single_Weight_Data =>
    new List<object[]>
    {
            new object[] 
            { 
                new List<WeightPlateModel> 
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 1 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 1 },
                },
                0.5 
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 1 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 1 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                1.25
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 1 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 1 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                10
            },
    };

    [Theory]
    [MemberData(nameof(When_Matching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_Single_Weight_Data))]
    public void When_Matching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_Single_Weight(List<WeightPlateModel> availableWeights, List<WeightPlateModel> expectedWeights, double targetWeight)
    {
        // Arrange
        List<WeightPlateModel> result = new()
        {
            new WeightPlateModel{ Weight = 20, Count = 0 },
            new WeightPlateModel{ Weight = 10, Count = 0 },
            new WeightPlateModel{ Weight = 5, Count = 0 },
            new WeightPlateModel{ Weight = 2.5, Count = 0 },
            new WeightPlateModel{ Weight = 1.25, Count = 0 },
            new WeightPlateModel{ Weight = 0.5, Count = 0 },
        };

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(availableWeights,
                                                        10,
                                                        targetWeight,
                                                        result);

        // Assert
        for (int i = 0; i < expectedWeights.Count(); i++)
        {
            Assert.Equal(expectedWeights[i].Weight, result[i].Weight);
            Assert.Equal(expectedWeights[i].Count, result[i].Count);
        }
    }

    public static IEnumerable<object[]> When_Mismatching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_No_Weights_Data =>
    new List<object[]>
    {
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                1.25,
                10
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 6 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                10,
                10
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 2 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                3,
                10
            }
    };

    [Theory]
    [MemberData(nameof(When_Mismatching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_No_Weights_Data))]
    public void When_Mismatching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_No_Weights(List<WeightPlateModel> availableWeights, List<WeightPlateModel> expectedWeights, double targetWeight, int maxPlates)
    {
        // Arrange
        List<WeightPlateModel> result = new()
        {
            new WeightPlateModel{ Weight = 20, Count = 0 },
            new WeightPlateModel{ Weight = 10, Count = 0 },
            new WeightPlateModel{ Weight = 5, Count = 0 },
            new WeightPlateModel{ Weight = 2.5, Count = 0 },
            new WeightPlateModel{ Weight = 1.25, Count = 0 },
            new WeightPlateModel{ Weight = 0.5, Count = 0 },
        };

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(availableWeights,
                                                        maxPlates,
                                                        targetWeight,
                                                        result);

        // Assert
        for (int i = 0; i < expectedWeights.Count(); i++)
        {
            Assert.Equal(expectedWeights[i].Weight, result[i].Weight);
            Assert.Equal(expectedWeights[i].Count, result[i].Count);
        }
    }

    public static IEnumerable<object[]> When_Target_Weight_Cannot_Be_Reached_GetPlatesForTargetWeight_Should_Return_No_Weights_Data =>
    new List<object[]>
    {
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 1 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 2 },
                    new WeightPlateModel{ Weight = 1.25, Count = 3 },
                    new WeightPlateModel{ Weight = 0.5, Count = 3 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                20,
                10
            }
    };

    [Theory]
    [MemberData(nameof(When_Target_Weight_Cannot_Be_Reached_GetPlatesForTargetWeight_Should_Return_No_Weights_Data))]
    public void When_Target_Weight_Cannot_Be_Reached_GetPlatesForTargetWeight_Should_Return_No_Weights(List<WeightPlateModel> availableWeights, List<WeightPlateModel> expectedWeights, double targetWeight, int maxPlates)
    {
        // Arrange
        List<WeightPlateModel> result = new()
        {
            new WeightPlateModel{ Weight = 20, Count = 0 },
            new WeightPlateModel{ Weight = 10, Count = 0 },
            new WeightPlateModel{ Weight = 5, Count = 0 },
            new WeightPlateModel{ Weight = 2.5, Count = 0 },
            new WeightPlateModel{ Weight = 1.25, Count = 0 },
            new WeightPlateModel{ Weight = 0.5, Count = 0 },
        };

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(availableWeights,
                                                        maxPlates,
                                                        targetWeight,
                                                        result);

        // Assert
        for (int i = 0; i < expectedWeights.Count(); i++)
        {
            Assert.Equal(expectedWeights[i].Weight, result[i].Weight);
            Assert.Equal(expectedWeights[i].Count, result[i].Count);
        }
    }

    public static IEnumerable<object[]> When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest_Data =>
        new List<object[]>
        {
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 5 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                2.5,
                10
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 1 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 4 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 1 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
                },
                10,
                10
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 2 },
                    new WeightPlateModel{ Weight = 0.5, Count = 1 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 1 },
                },
                3,
                10
            }
        };

    [Theory]
    [MemberData(nameof(When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest_Data))]
    public void When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest(List<WeightPlateModel> availableWeights, List<WeightPlateModel> expectedWeights, double targetWeight, int maxPlates)
    {
        // Arrange
        List<WeightPlateModel> result = new()
        {
            new WeightPlateModel{ Weight = 20, Count = 0 },
            new WeightPlateModel{ Weight = 10, Count = 0 },
            new WeightPlateModel{ Weight = 5, Count = 0 },
            new WeightPlateModel{ Weight = 2.5, Count = 0 },
            new WeightPlateModel{ Weight = 1.25, Count = 0 },
            new WeightPlateModel{ Weight = 0.5, Count = 0 },
        };

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(availableWeights,
                                                        maxPlates,
                                                        targetWeight,
                                                        result);

        // Assert
        for (int i = 0; i < expectedWeights.Count(); i++)
        {
            Assert.Equal(expectedWeights[i].Weight, result[i].Weight);
            Assert.Equal(expectedWeights[i].Count, result[i].Count);
        }
    }

    public static IEnumerable<object[]> When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination_Data =>
    new List<object[]>
    {
        new object[]
        {
            new List<WeightPlateModel>
            {
                new WeightPlateModel{ Weight = 20, Count = 0 },
                new WeightPlateModel{ Weight = 10, Count = 0 },
                new WeightPlateModel{ Weight = 5, Count = 0 },
                new WeightPlateModel{ Weight = 2.5, Count = 1 },
                new WeightPlateModel{ Weight = 1.25, Count = 0 },
                new WeightPlateModel{ Weight = 0.5, Count = 5 },
            },
            new List<WeightPlateModel>
            {
                new WeightPlateModel{ Weight = 20, Count = 0 },
                new WeightPlateModel{ Weight = 10, Count = 0 },
                new WeightPlateModel{ Weight = 5, Count = 0 },
                new WeightPlateModel{ Weight = 2.5, Count = 0 },
                new WeightPlateModel{ Weight = 1.25, Count = 0 },
                new WeightPlateModel{ Weight = 0.5, Count = 0 },
            },
            1.5,
            2
        },
        new object[]
        {
            new List<WeightPlateModel>
            {
                new WeightPlateModel{ Weight = 20, Count = 0 },
                new WeightPlateModel{ Weight = 10, Count = 4 },
                new WeightPlateModel{ Weight = 5, Count = 0 },
                new WeightPlateModel{ Weight = 2.5, Count = 0 },
                new WeightPlateModel{ Weight = 1.25, Count = 0 },
                new WeightPlateModel{ Weight = 0.5, Count = 0 },
            },
            new List<WeightPlateModel>
            {
                new WeightPlateModel{ Weight = 20, Count = 0 },
                new WeightPlateModel{ Weight = 10, Count = 0 },
                new WeightPlateModel{ Weight = 5, Count = 0 },
                new WeightPlateModel{ Weight = 2.5, Count = 0 },
                new WeightPlateModel{ Weight = 1.25, Count = 0 },
                new WeightPlateModel{ Weight = 0.5, Count = 0 },
            },
            40,
            3
        },
    };

    [Theory]
    [MemberData(nameof(When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination_Data))]
    public void When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination(List<WeightPlateModel> availableWeights, List<WeightPlateModel> expectedWeights, double targetWeight, int maxPlates)
    {
        // Arrange
        List<WeightPlateModel> result = new()
        {
            new WeightPlateModel{ Weight = 20, Count = 0 },
            new WeightPlateModel{ Weight = 10, Count = 0 },
            new WeightPlateModel{ Weight = 5, Count = 0 },
            new WeightPlateModel{ Weight = 2.5, Count = 0 },
            new WeightPlateModel{ Weight = 1.25, Count = 0 },
            new WeightPlateModel{ Weight = 0.5, Count = 0 },
        };

        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        weightPlatesCalculator.GetPlatesForTargetWeight(availableWeights,
                                                        maxPlates,
                                                        targetWeight,
                                                        result);

        // Assert
        for (int i = 0; i < expectedWeights.Count(); i++)
        {
            Assert.Equal(expectedWeights[i].Weight, result[i].Weight);
            Assert.Equal(expectedWeights[i].Count, result[i].Count);
        }
    }

    public static IEnumerable<object[]> GetPlatesForTargetWeight_Should_Return_Combination_In_Less_Than_One_Second_Data =>
        new List<object[]>
        {
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 1 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 1 },
                },
                0.5,
                10
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 2 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 1 },
                    new WeightPlateModel{ Weight = 0.5, Count = 3 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 2 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 1 },
                    new WeightPlateModel{ Weight = 0.5, Count = 3 },
                },
                45.25,
                10
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 1 },
                    new WeightPlateModel{ Weight = 5, Count = 1 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 1 },
                    new WeightPlateModel{ Weight = 0.5, Count = 3 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 1 },
                    new WeightPlateModel{ Weight = 5, Count = 1 },
                    new WeightPlateModel{ Weight = 2.5, Count = 1 },
                    new WeightPlateModel{ Weight = 1.25, Count = 1 },
                    new WeightPlateModel{ Weight = 0.5, Count = 3 },
                },
                20.25,
                10
            },
            new object[]
            {
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 10 },
                    new WeightPlateModel{ Weight = 10, Count = 10 },
                    new WeightPlateModel{ Weight = 5, Count = 10 },
                    new WeightPlateModel{ Weight = 2.5, Count = 10 },
                    new WeightPlateModel{ Weight = 1.25, Count = 10 },
                    new WeightPlateModel{ Weight = 0.5, Count = 10 },
                },
                new List<WeightPlateModel>
                {
                    new WeightPlateModel{ Weight = 20, Count = 0 },
                    new WeightPlateModel{ Weight = 10, Count = 0 },
                    new WeightPlateModel{ Weight = 5, Count = 0 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 4 },
                },
                2,
                10
            }
        };

    [Theory]
    [MemberData(nameof(GetPlatesForTargetWeight_Should_Return_Combination_In_Less_Than_One_Second_Data))]
    public void GetPlatesForTargetWeight_Should_Return_Combination_In_Less_Than_One_Second(List<WeightPlateModel> availableWeights, List<WeightPlateModel> expectedWeights, double targetWeight, int maxPlates)
    {
        // Arrange
        List<WeightPlateModel> result = new()
        {
            new WeightPlateModel{ Weight = 20, Count = 0 },
            new WeightPlateModel{ Weight = 10, Count = 0 },
            new WeightPlateModel{ Weight = 5, Count = 0 },
            new WeightPlateModel{ Weight = 2.5, Count = 0 },
            new WeightPlateModel{ Weight = 1.25, Count = 0 },
            new WeightPlateModel{ Weight = 0.5, Count = 0 },
        };
        var weightPlatesCalculator = new WeightPlatesProcessor();

        // Act
        long startTime = Stopwatch.GetTimestamp();
        weightPlatesCalculator.GetPlatesForTargetWeight(availableWeights,
                                                        maxPlates,
                                                        targetWeight,
                                                        result);
        TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTime);

        // Assert
        for (int i = 0; i < expectedWeights.Count(); i++)
        {
            Assert.Equal(expectedWeights[i].Weight, result[i].Weight);
            Assert.Equal(expectedWeights[i].Count, result[i].Count);
        }
        Assert.InRange(elapsedTime, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 1));
    }
}
