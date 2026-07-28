using System.Collections;
using UnityEngine;

public class CountingSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск сортировки подсчётом.");

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

        visualizer.ShowStatus($"Максимальный элемент = {max}.");

        visualizer.HighlightPseudoCodeLine(1);
        yield return SortingHelper.WaitForNextStep();

        int[] count = new int[max + 1];

        visualizer.ShowStatus("Создан массив подсчёта.");

        visualizer.HighlightPseudoCodeLine(2);
        yield return SortingHelper.WaitForNextStep();

        for (int i = 0; i < array.Length; i++)
        {
            count[array[i]]++;

            visualizer.ShowStatus(
                $"Количество элементов со значением {array[i]} увеличено."
            );

            yield return SortingHelper.WaitForNextStep();
        }

        visualizer.HighlightPseudoCodeLine(3);
        yield return SortingHelper.WaitForNextStep();

        for (int i = 1; i < count.Length; i++)
        {
            count[i] += count[i - 1];
        }

        visualizer.ShowStatus("Префиксные суммы вычислены.");

        yield return SortingHelper.WaitForNextStep();

        visualizer.HighlightPseudoCodeLine(4);

        int[] output = new int[array.Length];

        for (int i = array.Length - 1; i >= 0; i--)
        {
            output[count[array[i]] - 1] = array[i];
            count[array[i]]--;

            counter.IncrementSwaps();

            visualizer.ShowStatus(
                $"Элемент {array[i]} помещён в выходной массив."
            );

            yield return SortingHelper.WaitForNextStep();
        }

        visualizer.HighlightPseudoCodeLine(5);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = output[i];

            yield return visualizer.UpdateBar(i, output[i]);

            visualizer.MarkSorted(i);

            yield return SortingHelper.WaitForNextStep();
        }

        visualizer.ShowStatus("Сортировка подсчётом завершена.");
    }
}