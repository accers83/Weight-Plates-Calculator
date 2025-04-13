using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculatorLibrary.Helpers;

public static class WeightHelpers
{
    public static List<WeightPlateModel> DivideCountByTwo(this List<WeightPlateModel> weightsAvailable)
    {
        List<WeightPlateModel> output = new();

        foreach (var weight in weightsAvailable)
        {
            WeightPlateModel weightTemp = new();
            weightTemp.Count = weight.Count / 2;
            weightTemp.Weight = weight.Weight;
            output.Add(weightTemp);
        }

        return output;
    }
}
