using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculatorLibrary.Processors;

public class WeightPlatesProcessor : IWeightPlatesProcessor
{
    public void GetPlatesForTargetWeight(List<WeightPlateModel> weightPlates,
                                     int maxPlates,
                                     double targetWeight,
                                     List<WeightPlateModel> combination)
    {
        if (targetWeight <= 0)
        {
            throw new ArgumentOutOfRangeException("Target Weight", "Must be greater than 0.");
        }

        var fractionalDigits = targetWeight - Math.Truncate(targetWeight);
        var modulasRemainder = fractionalDigits % 0.25;

        if (modulasRemainder != 0)
        {
            throw new ArgumentOutOfRangeException("Target Weight", "Must be divisible by 0.25 with no remainder.");
        }

        maxPlates = maxPlates <= 25 ? maxPlates : 25;
        weightPlates = weightPlates.OrderByDescending(x => x.Weight).ToList();
        double sumWeight = 0;

        GetPlatesForTargetWeight(weightPlates,
                                          maxPlates,
                                          targetWeight,
                                          combination,
                                          sumWeight,
                                          0);
    }

    private void GetPlatesForTargetWeight(List<WeightPlateModel> weightPlates,
                                         int maxPlates,
                                         double targetWeight,
                                         List<WeightPlateModel> combination,
                                         double sumWeight,
                                         int index)
    {
        if (index >= weightPlates.Count())
        {
            return;
        }

        int plateCount = 0;
        for (var j = 0; j < weightPlates[index].Count; j++)
        {
            plateCount = 0;
            combination.ForEach(x => plateCount += x.Count);
            if (plateCount >= maxPlates)
            {
                break;
            }

            if (weightPlates[index].Count - combination[index].Count <= 0)
            {
                break;
            }

            sumWeight += weightPlates[index].Weight;

            if (sumWeight <= targetWeight)
            {
                combination.Where(x => x.Weight == weightPlates[index].Weight).First().Count++;
            }
            else
            {
                sumWeight -= weightPlates[index].Weight;
                break;
            }
        }

        if (sumWeight == targetWeight)
        {
            return;
        }

        plateCount = 0;
        combination.ForEach(x => plateCount += x.Count);
        if (sumWeight < targetWeight && plateCount == maxPlates)
        {
            combination.ForEach(x => x.Count = 0);
            sumWeight = 0;
            return;
        }

        if (index == (weightPlates.Count() - 1) && sumWeight < targetWeight)
        {
            (index, sumWeight) = BacktrackPlateCombination(combination,
                                             sumWeight,
                                             index,
                                             1);
        }

        GetPlatesForTargetWeight(weightPlates,
                                  maxPlates,
                                  targetWeight,
                                  combination,
                                  sumWeight,
                                  index + 1);
    }

    private (int index, double sumWeight) BacktrackPlateCombination(List<WeightPlateModel> combination, double sumWeight, int index, int backtrackCount)
    {
        var indexMinusBacktrack = index - backtrackCount;
        if (indexMinusBacktrack == 0 && (combination[0].Count == 0))
        {
            sumWeight = 0;
            foreach (var item in combination)
            {
                item.Count = 0;
            }
        }
        else if (combination[indexMinusBacktrack].Count > 0)
        {
            sumWeight -= combination[indexMinusBacktrack].Weight;
            combination[indexMinusBacktrack].Count--;

            for (int i = 0; i < backtrackCount; i++)
            {
                var indexMinusI = index - i;
                sumWeight -= combination[indexMinusI].Weight * combination[indexMinusI].Count;
                combination[indexMinusI].Count = 0;
            }

            index = indexMinusBacktrack;
        }
        else if (combination[indexMinusBacktrack].Count == 0)
        {
            (index, sumWeight) = BacktrackPlateCombination(combination, sumWeight, index, backtrackCount + 1);
        }

        return (index, sumWeight);
    }
}
