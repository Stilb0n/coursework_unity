using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadixMSDSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск Radix Sort (MSD).");

        if (array.Length == 0)
            yield break;

        int max = array[0];

        for (int i = 1; i < array.Length; i++)
        {
            counter.IncrementComparisons();

            if (array[i] > max)
                max = array[i];
        }

        int exp = 1;

        while (max / exp >= 10)
            exp *= 10;

        yield return MSDSort(array, 0, array.Length - 1, exp, visualizer, counter);

        for (int i = 0; i < array.Length; i++)
            visualizer.MarkSorted(i);

        visualizer.HighlightPseudoCodeLine(5);
        visualizer.ShowStatus("Radix MSD завершён.");
    }

    private IEnumerator MSDSort(int[] array,
                                int left,
                                int right,
                                int exp,
                                IVisualizerAPI visualizer,
                                IOperationCounter counter)
    {
        if (left >= right || exp == 0)
            yield break;

        visualizer.HighlightPseudoCodeLine(0);
        yield return SortingHelper.WaitForNextStep();

        List<int>[] buckets = new List<int>[10];

        for (int i = 0; i < 10; i++)
            buckets[i] = new List<int>();

        visualizer.HighlightPseudoCodeLine(1);
        visualizer.ShowStatus($"Разделяем диапазон [{left};{right}] по разряду {exp}.");
        yield return SortingHelper.WaitForNextStep();

        for (int i = left; i <= right; i++)
        {
            int digit = (array[i] / exp) % 10;

            buckets[digit].Add(array[i]);

            counter.IncrementSwaps();
        }

        visualizer.HighlightPseudoCodeLine(2);
        yield return SortingHelper.WaitForNextStep();

        int index = left;

        int[] bucketStart = new int[10];
        int[] bucketEnd = new int[10];

        for (int b = 0; b < 10; b++)
        {
            bucketStart[b] = index;

            foreach (int value in buckets[b])
            {
                array[index] = value;

                yield return visualizer.UpdateBar(index, value);

                visualizer.ShowStatus(
                    $"Записываем {value} в позицию {index}."
                );

                yield return SortingHelper.WaitForNextStep();

                index++;
            }

            bucketEnd[b] = index - 1;
        }

        visualizer.HighlightPseudoCodeLine(3);

        yield return SortingHelper.WaitForNextStep();

        if (exp == 1)
            yield break;

        visualizer.HighlightPseudoCodeLine(4);

        yield return SortingHelper.WaitForNextStep();

        for (int b = 0; b < 10; b++)
        {
            if (bucketStart[b] < bucketEnd[b])
            {
                yield return MSDSort(
                    array,
                    bucketStart[b],
                    bucketEnd[b],
                    exp / 10,
                    visualizer,
                    counter
                );
            }
        }
    }
}