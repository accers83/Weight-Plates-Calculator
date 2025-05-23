﻿using WeightPlatesCalculatorLibrary.Helpers;
using WeightPlatesCalculatorLibrary.Models;
using WeightPlatesCalculatorLibrary.Processors;

namespace WeightPlatesCalculatorLibrary.Services;

public class WeightPlatesService : IWeightPlatesService
{
    private readonly IWeightPlatesProcessor _weightPlatesProcessor;

    public WeightPlatesService(IWeightPlatesProcessor weightPlatesProcessor)
    {
        _weightPlatesProcessor = weightPlatesProcessor;
    }

    public void Initiate(WeightCalculationModel weightCalculation)
    {
        List<WeightPlateModel> weightsAvailable = new();
        int maxPlatesPerEnd = 0;
        double targetWeight = 0;

        if (weightCalculation.LiftingDeviceSelected == LiftingDeviceEndsOption.Double)
        {
            bool hasMinimumWeights = false;
            foreach (var weight in weightCalculation.WeightsAvailable)
            {
                hasMinimumWeights = weight.Count >= 2 ? true : false;

                if (hasMinimumWeights)
                {
                    break;
                }
            }

            if (hasMinimumWeights == false)
            {
                throw new Exception("Double ended lifting devices require at least 1 pair of matching weights.");
            }

            maxPlatesPerEnd = weightCalculation.LiftingDevicesAvailable.Where(x => x.EndsCount == LiftingDeviceEndsOption.Double).First().MaxPlatesPerEnd;
            weightsAvailable = weightCalculation.WeightsAvailable.DivideCountByTwo();
            targetWeight = weightCalculation.TargetWeight / 2;
        }
        else
        {
            bool hasMinimumWeights = false;
            foreach (var weight in weightCalculation.WeightsAvailable)
            {
                hasMinimumWeights = weight.Count >= 1 ? true : false;

                if (hasMinimumWeights)
                {
                    break;
                }
            }

            if (hasMinimumWeights == false)
            {
                throw new Exception("Single ended lifting devices requires at least 1 weight.");
            }

            maxPlatesPerEnd = weightCalculation.LiftingDevicesAvailable.Where(x => x.EndsCount == LiftingDeviceEndsOption.Single).First().MaxPlatesPerEnd;
            weightsAvailable = weightCalculation.WeightsAvailable;
            targetWeight = weightCalculation.TargetWeight;
        }

        _weightPlatesProcessor.GetPlatesForTargetWeight(weightsAvailable,
                                                        maxPlatesPerEnd,
                                                        targetWeight,
                                                        weightCalculation.WeightsSelectedPerEnd);
    }
}
