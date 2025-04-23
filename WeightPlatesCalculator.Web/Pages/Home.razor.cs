using WeightPlatesCalculator.Web.Helpers;
using WeightPlatesCalculator.Web.Models;

namespace WeightPlatesCalculator.Web.Pages
{
    public partial class Home
    {
        private bool displayPersonaliseForm = false;
        private bool savePersonalisationsDisabled = true;
        private bool deletePersonalisationsDisabled = true;
        private WeightCalculationUiModel? savedWeightCalculation;
        private WeightCalculationUiModel newWeightCalculation = new();
        private string weightPlateCalculatorErrorMessage = string.Empty;
        private bool weightPlateCalculatorErrorMessageHidden = true;
        private bool targetWeightAchieved = false;

        protected override async Task OnInitializedAsync()
        {
            savedWeightCalculation = await localStorage.GetItemAsync<WeightCalculationUiModel>("weightCalculation");

            if (savedWeightCalculation is null)
            {
                savedWeightCalculation = WeightPlatesHelper.GetUiDefaultWeightCalculation();
            }
            else
            {
                deletePersonalisationsDisabled = false;
            }

            newWeightCalculation = savedWeightCalculation;
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
            newWeightCalculation = WeightPlatesHelper.GetUiDefaultWeightCalculation();
            deletePersonalisationsDisabled = true;
        }

        private void ToggleDisplayPersonaliseForm()
        {
            displayPersonaliseForm = displayPersonaliseForm ? false : true;
        }

        private void CalculateRequiredWeightPlates()
        {
            targetWeightAchieved = false;
            var libraryWeightCalculation = newWeightCalculation.ToLibraryWeightCalculation();

            try
            {
                weightPlatesService.Initiate(libraryWeightCalculation);
                weightPlateCalculatorErrorMessage = string.Empty;
                weightPlateCalculatorErrorMessageHidden = true;
            }
            catch (Exception ex)
            {
                weightPlateCalculatorErrorMessage = ex.Message;
                weightPlateCalculatorErrorMessageHidden = false;
            }

            (targetWeightAchieved, newWeightCalculation.WeightsSelectedPerEnd) = libraryWeightCalculation.WeightsSelectedPerEnd.ToUiWeightsSelectedPerEnd();

            if (targetWeightAchieved == false && string.IsNullOrWhiteSpace(weightPlateCalculatorErrorMessage))
            {
                weightPlateCalculatorErrorMessage = "Target weight not achieved";
                weightPlateCalculatorErrorMessageHidden = false;
            }
        }
    }
}