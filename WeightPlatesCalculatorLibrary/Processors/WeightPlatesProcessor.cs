using WeightPlatesCalculatorLibrary.Models;

namespace WeightPlatesCalculatorLibrary.Processors;

public class WeightPlatesProcessor : IWeightPlatesProcessor
{

    public void GetPlatesForTargetWeight(List<WeightPlateModel> weightPlates,
                                         int maxPlates,
                                         double targetWeight,
                                         List<WeightPlateModel> combination)
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
        weightPlates = weightPlates.OrderByDescending(x => x.Weight).ToList();
        double sumWeight = 0;
        var plateCount = 0;

        for (int i = 0; i < weightPlates.Count(); i++)
        {
            for (var j = 0; j < weightPlates.Count; j++)
            {

                plateCount = 0;
                combination.ForEach(x => plateCount += x.Count);
                if (plateCount >= maxPlates)
                {
                    break;
                }

                if (weightPlates[i].Count - combination[i].Count <= 0)
                {
                    break;
                }

                sumWeight += weightPlates[i].Weight;

                if (sumWeight <= targetWeight)
                {
                    combination.Where(x => x.Weight == weightPlates[i].Weight).First().Count++;
                }
                else
                {
                    sumWeight -= weightPlates[i].Weight;
                    break;
                }
            }

            if (sumWeight == targetWeight)
            {
                break;
            }

            plateCount = 0;
            combination.ForEach(x => plateCount += x.Count);
            if (sumWeight < targetWeight && plateCount == maxPlates)
            {
                combination.ForEach(x => x.Count = 0);
                sumWeight = 0;
                break;
            }

            if (i == (weightPlates.Count - 1) && sumWeight < targetWeight)
            {
                if (combination[i - 1].Count > 0)
                {
                    sumWeight -= combination[i - 1].Weight;
                    combination[i - 1].Count--;

                    sumWeight -= combination[i].Weight * combination[i].Count;
                    combination[i].Count = 0;

                    i -= 1;
                }
                else
                {
                    if (combination[i - 2].Count > 0)
                    {
                        sumWeight -= combination[i - 2].Weight;
                        combination[i - 2].Count--;

                        sumWeight -= combination[i - 1].Weight * combination[i - 1].Count;
                        combination[i - 1].Count = 0;

                        sumWeight -= combination[i].Weight * combination[i].Count;
                        combination[i].Count = 0;

                        i -= 2;
                    }
                    else
                    {
                        if (combination[i - 3].Count > 0)
                        {
                            sumWeight -= combination[i - 3].Weight;
                            combination[i - 3].Count--;

                            sumWeight -= combination[i - 2].Weight * combination[i - 2].Count;
                            combination[i - 2].Count = 0;

                            sumWeight -= combination[i - 1].Weight * combination[i - 1].Count;
                            combination[i - 1].Count = 0;

                            sumWeight -= combination[i].Weight * combination[i].Count;
                            combination[i].Count = 0;

                            i -= 3;
                        }
                        else
                        {
                            if (combination[i - 4].Count > 0)
                            {
                                sumWeight -= combination[i - 4].Weight;
                                combination[i - 4].Count--;

                                sumWeight -= combination[i - 3].Weight * combination[i - 3].Count;
                                combination[i - 3].Count = 0;

                                sumWeight -= combination[i - 2].Weight * combination[i - 2].Count;
                                combination[i - 2].Count = 0;

                                sumWeight -= combination[i - 1].Weight * combination[i - 1].Count;
                                combination[i - 1].Count = 0;

                                sumWeight -= combination[i].Weight * combination[i].Count;
                                combination[i].Count = 0;

                                i -= 4;
                            }
                            else
                            {
                                if (combination[i - 5].Count > 0)
                                {
                                    sumWeight -= combination[i - 5].Weight;
                                    combination[i - 5].Count--;

                                    sumWeight -= combination[i - 4].Weight * combination[i - 4].Count;
                                    combination[i - 4].Count = 0;

                                    sumWeight -= combination[i - 3].Weight * combination[i - 3].Count;
                                    combination[i - 3].Count = 0;

                                    sumWeight -= combination[i - 2].Weight * combination[i - 2].Count;
                                    combination[i - 2].Count = 0;

                                    sumWeight -= combination[i - 1].Weight * combination[i - 1].Count;
                                    combination[i - 1].Count = 0;

                                    sumWeight -= combination[i].Weight * combination[i].Count;
                                    combination[i].Count = 0;

                                    i -= 5;
                                }
                                else
                                {
                                    sumWeight = 0;
                                    foreach (var item in combination)
                                    {
                                        item.Count = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
