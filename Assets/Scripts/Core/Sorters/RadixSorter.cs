using System.Collections;
using UnityEngine;

public class RadixSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск поразрядной сортировки.");

        if (array.Length == 0)
            yield break;

        visualizer.HighlightPseudoCodeLine(0);
        yield return SortingHelper.WaitForNextStep();

        int max = array[0];

        for (int i = 1; i < array.Length; i++)
        {
            counter.IncrementComparisons();

            if (array[i] > max)
                max = array[i];
        }

        visualizer.ShowStatus($"Максимальное число = {max}");

        for (int exp = 1; max / exp > 0; exp *= 10)
        {
            visualizer.HighlightPseudoCodeLine(1);

visualizer.ShowStatus(
    $"Сортировка по разряду {(exp == 1 ? "единиц" : exp == 10 ? "десятков" : exp == 100 ? "сотен" : exp.ToString())}."
);

            yield return SortingHelper.WaitForNextStep();

            yield return CountingSortByDigit(array, exp, visualizer, counter);

            visualizer.HighlightPseudoCodeLine(3);

            visualizer.ShowStatus("Переходим к следующему разряду.");

            yield return SortingHelper.WaitForNextStep();
        }

        visualizer.HighlightPseudoCodeLine(5);

        for (int i = 0; i < array.Length; i++)
        {
            visualizer.MarkSorted(i);
        }

        visualizer.ShowStatus("Radix Sort завершён.");
    }

    private IEnumerator CountingSortByDigit(int[] array,
                                            int exp,
                                            IVisualizerAPI visualizer,
                                            IOperationCounter counter)
    {
        int n = array.Length;

        int[] output = new int[n];
        int[] count = new int[10];

        visualizer.HighlightPseudoCodeLine(2);
        yield return SortingHelper.WaitForNextStep();

        for (int i = 0; i < n; i++)
        {
            int digit = (array[i] / exp) % 10;
            count[digit]++;
        }

        for (int i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
        }

        for (int i = n - 1; i >= 0; i--)
        {
            int digit = (array[i] / exp) % 10;

            output[count[digit] - 1] = array[i];

            count[digit]--;

            counter.IncrementSwaps();
        }

        visualizer.HighlightPseudoCodeLine(4);

        for (int i = 0; i < n; i++)
        {
            array[i] = output[i];

            yield return visualizer.UpdateBar(i, output[i]);

            visualizer.ShowStatus(
                $"Записываем {output[i]} обратно в массив."
            );

            yield return SortingHelper.WaitForNextStep();
        }
    }
}