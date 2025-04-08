
namespace WeightPlatesCalculatorLibrary.Processors
{
    public interface IWeightPlatesProcessor
    {
        void GetPlatesForTargetWeight(List<double> weights, int maxPlates, double targetWeight, List<double> weightCombination);
    }
}