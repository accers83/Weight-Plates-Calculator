using System.Diagnostics;
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
        private string weightPlateCalculatorErrorMessage = string.Empty;
        private bool weightPlateCalculatorErrorMessageHidden = true;
        private bool targetWeightAchieved = false;

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

            output.WeightsSelectedPerEnd = GetDefaultWeightsSelectedPerEnd();
            return output;
        }

        private List<WeightPlateUiModel> GetDefaultWeightsSelectedPerEnd()
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
            targetWeightAchieved = false;
            var weightCalculationTemp = CreateWeightCalculationModel();

            try
            {
                weightPlatesService.Initiate(weightCalculationTemp);
                weightPlateCalculatorErrorMessage = string.Empty;
                weightPlateCalculatorErrorMessageHidden = true;
            }
            catch (Exception ex)
            {
                weightPlateCalculatorErrorMessage = ex.Message;
                weightPlateCalculatorErrorMessageHidden = false;
            }

            newWeightCalculation.WeightsSelectedPerEnd = new();
            foreach (var item in weightCalculationTemp.WeightsSelectedPerEnd)
            {
                if (targetWeightAchieved == false && item.Count > 0)
                {
                    targetWeightAchieved = true;
                }

                WeightPlateUiModel weightPlateTemp = new() { Weight = item.Weight, Count = item.Count };
                newWeightCalculation.WeightsSelectedPerEnd.Add(weightPlateTemp);
            }

            if (targetWeightAchieved == false && string.IsNullOrWhiteSpace(weightPlateCalculatorErrorMessage))
            {
                weightPlateCalculatorErrorMessage = "Target weight not achieved";
                weightPlateCalculatorErrorMessageHidden = false;
            }
        }

        private WeightCalculationModel CreateWeightCalculationModel()
        {
            WeightCalculationModel output = new()
            {
                WeightsAvailable = new(),
                LiftingDevicesAvailable = new(),
                LiftingDeviceSelected = newWeightCalculation.LiftingDeviceSelected,
                TargetWeight = newWeightCalculation.TargetWeight,
                WeightsSelectedPerEnd = new()
            };

            foreach (var item in GetDefaultWeightsSelectedPerEnd())
            {
                WeightPlateModel weightPlateTemp = new() { Weight = item.Weight, Count = item.Count };
                output.WeightsSelectedPerEnd.Add(weightPlateTemp);
            }

            foreach (var item in newWeightCalculation.WeightsAvailable)
            {
                WeightPlateModel weightPlateTemp = new() { Weight = item.Weight, Count = item.Count };
                output.WeightsAvailable.Add(weightPlateTemp);
            }

            foreach (var item in newWeightCalculation.LiftingDevicesAvailable)
            {
                LiftingDeviceModel liftingDeviceTemp = new() { EndsCount = item.EndsCount, MaxPlatesPerEnd = item.MaxPlatesPerEnd };
                output.LiftingDevicesAvailable.Add(liftingDeviceTemp);
            }

            return output;
        }
    }
}