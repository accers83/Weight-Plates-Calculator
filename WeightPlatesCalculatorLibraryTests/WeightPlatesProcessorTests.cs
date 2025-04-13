using System.Diagnostics;
using WeightPlatesCalculatorLibrary.Models;
using WeightPlatesCalculatorLibrary.Processors;

namespace WeightPlatesCalculatorLibraryTests;

public class WeightPlatesProcessorTests
{
    //public static IEnumerable<object[]> When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest_Data =>
    //    new List<object[]>
    //    {
    //        new object[] { new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 2.5 }, new List<double> { 2.5 }, 2.5 },
    //        new object[] { new List<double> { 2.5, 2.5, 2.5, 2.5, 10 }, new List<double> { 10 }, 10 },
    //        new object[] { new List<double> { 0.5, 1.25, 1.25, 2.5 }, new List<double> { 0.5, 2.5 }, 3 }
    //    };

    //public static IEnumerable<object[]> When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination_Data =>
    //new List<object[]>
    //{
    //        new object[] { new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 2.5 }, new List<double> { }, 1.5, 2 },
    //        new object[] { new List<double> { 2.5, 2.5, 2.5, 2.5 }, new List<double> { }, 10, 3 },
    //        new object[] { new List<double> { 1.25, 2.5, 10, 10, 10 }, new List<double> { }, 33.75, 4 }
    //};

    //public static IEnumerable<object[]> GetPlatesForTargetWeight_Should_Return_Combination_In_Less_Than_One_Second_Data =>
    //new List<object[]>
    //{
    //        new object[] 
    //        { 
    //            new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5,
    //                               1.25, 1.25, 1.25, 1.25, 1.25,
    //                               2.5, 2.5, 2.5, 2.5, 2.5,
    //                               5, 5, 5, 5, 5,
    //                               10, 10, 10, 10, 10,
    //                               20, 20, 20, 20, 20
    //                               }, 
    //            new List<double> { 2.5 },
    //            2.5,
    //            5
    //        },
    //        new object[]
    //        {
    //            new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 
    //                               1.25, 1.25, 1.25, 1.25, 1.25, 1.25, 
    //                               2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 
    //                               5, 5, 5, 5, 5, 5, 
    //                               10, 10, 10, 10, 10, 10, 
    //                               20, 20, 20, 20, 20, 20,
    //                               },
    //            new List<double> { 2.5 },
    //            2.5,
    //            6
    //        },
    //        new object[]
    //        {
    //            new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5,
    //                               1.25, 1.25, 1.25, 1.25, 1.25, 1.25, 1.25,
    //                               2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5,
    //                               5, 5, 5, 5, 5, 5, 5,
    //                               10, 10, 10, 10, 10, 10, 10,
    //                               20, 20, 20, 20, 20, 20, 20
    //                               },
    //            new List<double> { 2.5 },
    //            2.5,
    //            7
    //        },
    //        new object[]
    //        {
    //            new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5,/* 0.5, 0.5,*/
    //                               1.25, 1.25,/* 1.25, 1.25, 1.25, 1.25, 1.25,*/
    //                               2.5, /*2.5, 2.5, 2.5, 2.5, 2.5, 2.5,*/
    //                               //5, 5, 5, 5, 5, 5, 5,
    //                               //10, 10, 10, 10, 10, 10, 10,
    //                               //20, 20, 20, 20, 20, 20, 20
    //                               },
    //            new List<double> { 2.5 },
    //            2.5,
    //            7
    //        },
    //        new object[]
    //        {
    //            new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5,
    //                               1.25, 1.25, 1.25, 1.25, 1.25, 1.25, 1.25,
    //                               2.5, 2.5, 2.5, 2.5, 2.5, /* 2.5, 2.5,*/
    //                               5, 5, /* 5, 5, 5, 5, 5,*/
    //                               10, /* 10, 10, 10, 10, 10, 10,*/
    //                               //20, 20, 20, 20, 20, 20, 20
    //                               },
    //            new List<double> { 0.5, 0.5, 0.5, 10 },
    //            11.5,
    //            7
    //        },
    //        new object[]
    //        {
    //            new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5,
    //                               1.25, 1.25, 1.25, 1.25, 1.25, 1.25, 1.25,
    //                               2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5,
    //                               5, 5, 5, 5, 5, 5, 5,
    //                               10, 10, 10, /*10, 10, 10, 10,*/
    //                               20, /*20, 20, 20, 20, 20, 20*/
    //                               },
    //            new List<double> { 5, 10, 20 },
    //            35,
    //            7
    //        },
    //        new object[]
    //        {
    //            new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5,
    //                               1.25, 1.25, 1.25, 1.25, 1.25, 1.25, 1.25,
    //                               2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5,
    //                               5, 5, 5, 5, 5, 5, 5,
    //                               10, 10, 10, 10,/* 10, 10, 10,*/
    //                               20, 20,/* 20, 20, 20, 20, 20*/
    //                               },
    //            new List<double> { 5, 20, 20 },
    //            45,
    //            7
    //        }

    //};

    //public static IEnumerable<object[]> When_TargetWeight_Not_Divisible_By_0Point25_With_Remainder_0_GetPlatesForTargetWeight_Should_Throw_Exception_Data =>
    //new List<object[]>
    //{
    //        new object[] { new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 2.5 }, 2.51 },
    //        new object[] { new List<double> { 2.5, 2.5, 2.5, 2.5, 10 }, 10.49 },
    //        new object[] { new List<double> { 0.5, 1.25, 1.25, 2.5 }, 3.09 },
    //        new object[] { new List<double> { 0.5, 1.25, 1.25, 2.5 }, 0.99999 },
    //        new object[] { new List<double> { 0.5, 1.25, 1.25, 2.5 }, 0.00001 },
    //};

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

    //[Theory]
    //[InlineData(0.5, 1.25)]
    //[InlineData(1.25, 0.5)]
    //[InlineData(1.25, 10)]
    //public void When_Mismatching_Target_And_Single_Weight_GetPlatesForTargetWeight_Should_Return_No_Weights(double weight, double targetWeight)
    //{
    //    // Arrange
    //    List<double> expected = new();

    //    List<double> result = new();

    //    var weightPlatesCalculator = new WeightPlatesProcessor();

    //    // Act
    //    weightPlatesCalculator.GetPlatesForTargetWeight(new List<double> { weight },
    //                                                    10,
    //                                                    targetWeight,
    //                                                    result);

    //    // Assert
    //    Assert.Equal(expected, result);
    //}

    //[Theory]
    //[MemberData(nameof(When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest_Data))]
    //public void When_Multiple_Eligible_Combinations_GetPlatesForTargetWeight_Should_Return_Smallest(List<double> weights, List<double> expected, double targetWeight)
    //{
    //    // Arrange
    //    List<double> result = new();

    //    var weightPlatesCalculator = new WeightPlatesProcessor();

    //    // Act
    //    weightPlatesCalculator.GetPlatesForTargetWeight(weights,
    //                                                    10,
    //                                                    targetWeight,
    //                                                    result);

    //    // Assert
    //    Assert.Equal(expected, result);
    //}

    //[Theory]
    //[MemberData(nameof(When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination_Data))]
    //public void When_MaxPlates_Less_Than_Combination_Count_GetPlatesForTargetWeight_Should_Not_Return_Combination(List<double> weights, List<double> expected, double targetWeight, int maxPlates)
    //{
    //    // Arrange
    //    List<double> result = new();

    //    var weightPlatesCalculator = new WeightPlatesProcessor();

    //    // Act
    //    weightPlatesCalculator.GetPlatesForTargetWeight(weights,
    //                                                    maxPlates,
    //                                                    targetWeight,
    //                                                    result);

    //    // Assert
    //    Assert.Equal(expected, result);
    //}

    //[Theory]
    //[MemberData(nameof(When_TargetWeight_Not_Divisible_By_0Point25_With_Remainder_0_GetPlatesForTargetWeight_Should_Throw_Exception_Data))]
    //public void When_TargetWeight_Not_Divisible_By_0Point25_With_Remainder_0_GetPlatesForTargetWeight_Should_Throw_Exception(List<double> weights, double targetWeight)
    //{
    //    // Arrange
    //    List<double> result = new();

    //    var weightPlatesCalculator = new WeightPlatesProcessor();

    //    // Act and Assert
    //    Assert.Throws<ArgumentOutOfRangeException>(() => weightPlatesCalculator.GetPlatesForTargetWeight(weights,
    //                                                    10,
    //                                                    targetWeight,
    //                                                    result));
    //}

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
                    new WeightPlateModel{ Weight = 5, Count = 1 },
                    new WeightPlateModel{ Weight = 2.5, Count = 0 },
                    new WeightPlateModel{ Weight = 1.25, Count = 0 },
                    new WeightPlateModel{ Weight = 0.5, Count = 0 },
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
