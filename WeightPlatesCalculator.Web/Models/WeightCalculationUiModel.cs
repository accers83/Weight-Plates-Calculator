using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculator.Web.Models;

public class WeightCalculationUiModel
{
    public double TargetWeight { get; set; } // TODO: rename to TargetWeightTotal?
    public bool CookiesAccepted { get; set; }
    public LiftingDeviceEndsOption LiftingDeviceSelected { get; set; } = LiftingDeviceEndsOption.Single;
    public List<LiftingDeviceUiModel> LiftingDevicesAvailable { get; set; } = new();
    public List<WeightPlateUiModel> WeightsSelectedPerEnd { get; set; } = new();
    public List<WeightPlateUiModel> WeightsAvailable { get; set; } = new();
}
