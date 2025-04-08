using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculatorLibrary.Helpers;

public static class WeightHelpers
{
    public static List<double> ToMaxPlatesLimitedListOfDouble(this List<WeightPlateModel> weightsAvailable, int maxPlatesPerEnd)
    {
        List<double> output = new();
        foreach (var weight in weightsAvailable)
        {
            List<double> weightsTemp = new();
            for (int i = 0; i < weight.Count; i++)
            {
                if (i == maxPlatesPerEnd - 1)
                {
                    break;
                }

                weightsTemp.Add(weight.Weight);
            }

            output.AddRange(weightsTemp);
        }

        return output;
    }

    public static List<WeightPlateModel> ToListOfWeightPlateModel(this List<double> weightsSelected)
    {
        List<WeightPlateModel> output = new();

        output = weightsSelected.Distinct().Select(x => new WeightPlateModel
        {
            Weight = x,
            Count = weightsSelected.Count(c => c == x),
        })
        .ToList();

        return output;
    }

    public static List<WeightPlateModel> DivideCountByTwo(this List<WeightPlateModel> weightsAvailable)
    {
        List<WeightPlateModel> output = new();

        foreach (var weight in weightsAvailable)
        {
            WeightPlateModel weightTemp = new();
            weightTemp.Count = weight.Count / 2;
            output.Add(weightTemp);
        }

        return output;
    }
}
