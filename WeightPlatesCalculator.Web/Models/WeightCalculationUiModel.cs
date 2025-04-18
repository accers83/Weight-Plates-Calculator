using System.ComponentModel.DataAnnotations;
using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculator.Web.Models;

public class WeightCalculationUiModel
{
    [Required]
    [Range(0.5, 100)]
    [Display(Name = "Target Weight")]
    public double TargetWeight { get; set; } // TODO: rename to TargetWeightTotal?
    public bool CookiesAccepted { get; set; } = false;
    public LiftingDeviceEndsOption LiftingDeviceSelected { get; set; } = LiftingDeviceEndsOption.Single;
    public List<LiftingDeviceUiModel> LiftingDevicesAvailable { get; set; } = new();
    public List<WeightPlateUiModel> WeightsSelectedPerEnd { get; set; } = new();
    public List<WeightPlateUiModel> WeightsAvailable { get; set; } = new();
}
