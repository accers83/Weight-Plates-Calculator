using System.Diagnostics;
using System.Threading.Tasks;
using WeightPlatesCalculator.Web.Models;
using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculator.Web.Pages
{
    public partial class Home
    {
        private bool displayPersonaliseForm = false;
        private bool savePersonalisationsDisabled = true;
        private bool deletePersonalisationsDisabled = true;
        private WeightCalculationUiModel? weightCalculation;
        private WeightCalculationUiModel newWeightCalculation = new();
        private TimeSpan elapsedTime = new();

        protected override async Task OnInitializedAsync()
        {
            weightCalculation = await localStorage.GetItemAsync<WeightCalculationUiModel>("weightCalculation");

            if (weightCalculation is null)
            {
                weightCalculation = GetDefaultWeightCalculation();
            }
            else
            {
                deletePersonalisationsDisabled = false;
            }

            newWeightCalculation = weightCalculation;
        }

        private WeightCalculationUiModel GetDefaultWeightCalculation()
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

            output.WeightsSelectedPerEnd = new()
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

        private async Task SavePersonalisationsAsync()
        {
            if (newWeightCalculation.CookiesAccepted)
            {
                await localStorage.SetItemAsync<WeightCalculationUiModel>("weightCalculation", newWeightCalculation);
                deletePersonalisationsDisabled = false;
            }
        }

        private async Task DeletePersonalisationAsync()
        {
            await localStorage.RemoveItemAsync("weightCalculation");
            newWeightCalculation = GetDefaultWeightCalculation();
            deletePersonalisationsDisabled = true;
        }

        private void ToggleDisplayPersonaliseForm()
        {
            displayPersonaliseForm = displayPersonaliseForm ? false : true;
        }

        private void CalculateRequiredWeightPlates()
        {
            WeightCalculationModel weightCalculationTemp = new()
            {
                WeightsAvailable = new(),
                LiftingDevicesAvailable = new(),
                LiftingDeviceSelected = newWeightCalculation.LiftingDeviceSelected,
                TargetWeight = newWeightCalculation.TargetWeight,
                WeightsSelectedPerEnd = new()
            };

            weightCalculationTemp.WeightsSelectedPerEnd = new()
            {
                new WeightPlateModel { Weight = 20, Count = 0 },
                new WeightPlateModel { Weight = 10, Count = 0 },
                new WeightPlateModel { Weight = 5, Count = 0 },
                new WeightPlateModel { Weight = 2.5, Count = 0 },
                new WeightPlateModel { Weight = 1.25, Count = 0 },
                new WeightPlateModel { Weight = 0.5, Count = 0 }
            };

            foreach (var item in newWeightCalculation.WeightsAvailable)
            {
                WeightPlateModel weightPlateTemp = new()
                {
                    Weight = item.Weight,
                    Count = item.Count
                };

                weightCalculationTemp.WeightsAvailable.Add(weightPlateTemp);
            }

            foreach (var item in newWeightCalculation.LiftingDevicesAvailable)
            {
                LiftingDeviceModel liftingDeviceTemp = new()
                {
                    EndsCount = item.EndsCount,
                    MaxPlatesPerEnd = item.MaxPlatesPerEnd
                };

                weightCalculationTemp.LiftingDevicesAvailable.Add(liftingDeviceTemp);
            }

            long startTime = Stopwatch.GetTimestamp();
            weightPlatesService.Initiate(weightCalculationTemp);
            elapsedTime = Stopwatch.GetElapsedTime(startTime);

            newWeightCalculation.WeightsSelectedPerEnd = new();

            foreach (var item in weightCalculationTemp.WeightsSelectedPerEnd)
            {
                WeightPlateUiModel weightPlateTemp = new()
                {
                    Weight = item.Weight,
                    Count = item.Count
                };

                newWeightCalculation.WeightsSelectedPerEnd.Add(weightPlateTemp);
            }
        }
    }
}