namespace WeightPlatesCalculatorLibrary.Processors;

public class WeightPlatesProcessor
{

    public void GetPlatesForTargetWeight(List<double> weights,
                                         int maxPlates,
                                         double targetWeight,
                                         List<double> weightCombination)
    {
        // target weight must be divisable by .25
        if (targetWeight <= 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        var fractionalDigits = targetWeight - Math.Truncate(targetWeight);
        var modulasRemainder = fractionalDigits % 0.25;

        if (modulasRemainder != 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        maxPlates = maxPlates <= 15 ? maxPlates : 15;


        // find all the distinct numbers in weights
        // any that have a count > than maxPlates, restrict to a count of maxPlates

        var distinctValues = weights.Distinct().ToList();

        var results = distinctValues.Select(x => new
        {
            Value = x,
            Count = weights.Count(c => c == x),
        });

        List<double> weightsSubset = new();
        foreach (var value in results)
        {
            int valueCount = value.Count <= maxPlates ? value.Count : maxPlates;
            for (int i = 0; i < valueCount; i++)
            {
                weightsSubset.Add(value.Value);
            }
        }


        Dictionary<string, List<double>> weightCombinations = new();


        GetPlatesForTargetWeight(weightsSubset,
                            0,
                            maxPlates,
                            targetWeight,
                            weightCombinations,
                            new List<double>());

        if (weightCombinations.Count() > 0)
        {
            List<List<double>> weightCombinationsTemp = new();
            foreach (var combination in weightCombinations)
            {
                List<double> combinationTemp = new();
                combination.Value.ForEach(x => combinationTemp.Add(x));
                weightCombinationsTemp.Add(combinationTemp);
            }

            var bestCombination = weightCombinationsTemp.OrderBy(x => x.Count()).First();
            weightCombination.AddRange(weightCombinationsTemp.OrderBy(x => x.Count()).First());
        }
    }

    private void GetPlatesForTargetWeight(List<double> weights,
                                          int index,
                                          int maxPlates,
                                          double targetWeight,
                                          Dictionary<string, List<double>> weightCombinations,
                                          List<double> subset)
    {
        double currentSubsetSum = 0;
        foreach (var weight in subset)
        {
            currentSubsetSum += weight;
        }

        if (currentSubsetSum == targetWeight)
        {
            var combinationKey = string.Empty;
            foreach (var weight in subset)
            {
                combinationKey += $"{weight},";
            }
            combinationKey = combinationKey.Remove(combinationKey.Length - 1);

            if (weightCombinations.ContainsKey(combinationKey) == false)
            {
                List<double> combination = new();
                subset.ForEach(x => combination.Add(x));
                weightCombinations.Add(combinationKey, combination);
            }
        }

        if (index > weights.Count() - 1)
        {
            return;
        }


        if (subset.Count() < maxPlates)
        {
            subset.Add(weights[index]);
            GetPlatesForTargetWeight(weights, index + 1, maxPlates, targetWeight, weightCombinations, subset);
            subset.RemoveAt(subset.Count() - 1);
        }

        GetPlatesForTargetWeight(weights, index + 1, maxPlates, targetWeight, weightCombinations, subset);
    }
}
