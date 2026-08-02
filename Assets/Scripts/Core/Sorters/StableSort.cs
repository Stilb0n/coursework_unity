using System.Collections;
using UnityEngine;

public class StableSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск std::stable_sort.");

        yield return MergeSort(
            array,
            0,
            array.Length - 1,
            visualizer,
            counter
        );

        for (int i = 0; i < array.Length; i++)
            visualizer.MarkSorted(i);

        visualizer.HighlightPseudoCodeLine(5);

        visualizer.ShowStatus("std::stable_sort завершён.");
    }

    private IEnumerator MergeSort(int[] array,
                                  int left,
                                  int right,
                                  IVisualizerAPI visualizer,
                                  IOperationCounter counter)
    {
        visualizer.HighlightPseudoCodeLine(0);

        yield return SortingHelper.WaitForNextStep();

        if (left >= right)
            yield break;

        int mid = (left + right) / 2;

        visualizer.HighlightPseudoCodeLine(1);

        visualizer.ShowStatus(
            $"Сортируем диапазон [{left}; {mid}]"
        );

        yield return SortingHelper.WaitForNextStep();

        yield return MergeSort(
            array,
            left,
            mid,
            visualizer,
            counter
        );

        visualizer.HighlightPseudoCodeLine(2);

        visualizer.ShowStatus(
            $"Сортируем диапазон [{mid + 1}; {right}]"
        );

        yield return SortingHelper.WaitForNextStep();

        yield return MergeSort(
            array,
            mid + 1,
            right,
            visualizer,
            counter
        );

        visualizer.HighlightPseudoCodeLine(3);

        visualizer.ShowStatus(
            "Сливаем две отсортированные части."
        );

        yield return SortingHelper.WaitForNextStep();

        yield return Merge(
            array,
            left,
            mid,
            right,
            visualizer,
            counter
        );
    }

    private IEnumerator Merge(int[] array,
                              int left,
                              int mid,
                              int right,
                              IVisualizerAPI visualizer,
                              IOperationCounter counter)
    {        int[] temp = new int[right - left + 1];

        int i = left;
        int j = mid + 1;
        int k = 0;

        while (i <= mid && j <= right)
        {
            visualizer.Highlight(left + k, j);

            counter.IncrementComparisons();

            yield return SortingHelper.WaitForNextStep();

            // Благодаря <= сортировка остаётся стабильной
            if (array[i] <= array[j])
            {
                temp[k++] = array[i++];
            }
            else
            {
                temp[k++] = array[j++];
            }
        }

        while (i <= mid)
            temp[k++] = array[i++];

        while (j <= right)
            temp[k++] = array[j++];

        for (k = 0; k < temp.Length; k++)
        {
            array[left + k] = temp[k];

            counter.IncrementSwaps();

            yield return visualizer.UpdateBar(left + k, temp[k]);

            yield return SortingHelper.WaitForNextStep();
        }

        visualizer.HighlightPseudoCodeLine(4);

        visualizer.ShowStatus(
            $"Диапазон [{left}; {right}] объединён."
        );

        yield return SortingHelper.WaitForNextStep();
    }
}