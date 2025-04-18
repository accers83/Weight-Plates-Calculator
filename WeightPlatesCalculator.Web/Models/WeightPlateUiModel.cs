using System.ComponentModel.DataAnnotations;

namespace WeightPlatesCalculator.Web.Models;

public class WeightPlateUiModel
{
    public double Weight { get; set; }
    [Required]
    [Range(0, 25)]
    public int Count { get; set; }
}
