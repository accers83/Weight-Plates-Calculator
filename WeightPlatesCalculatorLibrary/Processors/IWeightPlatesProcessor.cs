
using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculatorLibrary.Processors;

public interface IWeightPlatesProcessor
{
    //void GetPlatesForTargetWeight(List<double> weights, int maxPlates, double targetWeight, List<double> weightCombination);
    void GetPlatesForTargetWeight(List<WeightPlateModel> weightPlates, int maxPlates, double targetWeight, List<WeightPlateModel> combination);
}