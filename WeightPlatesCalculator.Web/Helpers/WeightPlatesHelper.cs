using WeightPlatesCalculator.Web.Models;
using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculator.Web.Helpers;

public static class WeightPlatesHelper
{
    public static WeightCalculationUiModel GetUiDefaultWeightCalculation()
    {
        WeightCalculationUiModel output = new();
         
        output.WeightsAvailable = new()
            {
                new WeightPlateUiModel { Weight = 20, Count = 10 },
                new WeightPlateUiModel { Weight = 10, Count = 10 },
                new WeightPlateUiModel { Weight = 5, Count = 10 },
                new WeightPlateUiModel { Weight = 2.5, Count = 10 },
                new WeightPlateUiModel { Weight = 1.25, Count = 10 },
                new WeightPlateUiModel { Weight = 0.5, Count = 10 }
            };

        output.LiftingDevicesAvailable = new()
            {
                new LiftingDeviceUiModel { EndsCount = LiftingDeviceEndsOption.Single, MaxPlatesPerEnd = 10 },
                new LiftingDeviceUiModel { EndsCount = LiftingDeviceEndsOption.Double, MaxPlatesPerEnd = 10 }
            };

        output.WeightsSelectedPerEnd = GetUiDefaultWeightsSelectedPerEnd();
        return output;
    }

    public static List<WeightPlateUiModel> GetUiDefaultWeightsSelectedPerEnd()
    {
        List<WeightPlateUiModel> output = new()
            {
                new WeightPlateUiModel { Weight = 20, Count = 0 },
                new WeightPlateUiModel { Weight = 10, Count = 0 },
                new WeightPlateUiModel { Weight = 5, Count = 0 },
                new WeightPlateUiModel { Weight = 2.5, Count = 0 },
                new WeightPlateUiModel { Weight = 1.25, Count = 0 },
                new WeightPlateUiModel { Weight = 0.5, Count = 0 }
            };

        return output;
    }

    public static WeightCalculationModel ToLibraryWeightCalculation(this WeightCalculationUiModel uiWeightCalculation)
    {
        WeightCalculationModel output = new()
        {
            WeightsAvailable = new(),
            LiftingDevicesAvailable = new(),
            LiftingDeviceSelected = uiWeightCalculation.LiftingDeviceSelected,
            TargetWeight = uiWeightCalculation.TargetWeight,
            WeightsSelectedPerEnd = new()
        };

        foreach (var item in GetUiDefaultWeightsSelectedPerEnd())
        {
            WeightPlateModel weightPlateTemp = new() { Weight = item.Weight, Count = item.Count };
            output.WeightsSelectedPerEnd.Add(weightPlateTemp);
        }

        foreach (var item in uiWeightCalculation.WeightsAvailable)
        {
            WeightPlateModel weightPlateTemp = new() { Weight = item.Weight, Count = item.Count };
            output.WeightsAvailable.Add(weightPlateTemp);
        }

        foreach (var item in uiWeightCalculation.LiftingDevicesAvailable)
        {
            LiftingDeviceModel liftingDeviceTemp = new() { EndsCount = item.EndsCount, MaxPlatesPerEnd = item.MaxPlatesPerEnd };
            output.LiftingDevicesAvailable.Add(liftingDeviceTemp);
        }

        return output;
    }

    public static (bool targetWeightAchieved, List<WeightPlateUiModel> uiWeightsSelectedPerEnd) ToUiWeightsSelectedPerEnd(this List<WeightPlateModel> libraryWeightsSelectedPerEnd)
    {
        bool targetWeightAchieved = false;
        List<WeightPlateUiModel> uiWeightsSelectedPerEnd = new();

        foreach (var item in libraryWeightsSelectedPerEnd)
        {
            if (targetWeightAchieved == false && item.Count > 0)
            {
                targetWeightAchieved = true;
            }

            WeightPlateUiModel weightPlateTemp = new() { Weight = item.Weight, Count = item.Count };
            uiWeightsSelectedPerEnd.Add(weightPlateTemp);
        }

        return (targetWeightAchieved, uiWeightsSelectedPerEnd);
    }
}
