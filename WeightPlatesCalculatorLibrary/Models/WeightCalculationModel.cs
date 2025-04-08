namespace WeightPlatesCalculatorLibrary.Models;

public class WeightCalculationModel
{
    public double TargetWeight { get; set; } // TODO: rename to TargetWeightTotal?
    public LiftingDeviceEndsOption LiftingDeviceSelected { get; set; }
    public List<LiftingDeviceModel> LiftingDevicesAvailable { get; set; }
    public List<WeightPlateModel> WeightsSelectedPerEnd { get; set; }
    public List<WeightPlateModel> WeightsAvailable { get; set; }
}
