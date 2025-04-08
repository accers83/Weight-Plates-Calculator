using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculatorLibrary.Services
{
    public interface IWeightPlatesService
    {
        void Initiate(WeightCalculationModel weightCalculation);
    }
}