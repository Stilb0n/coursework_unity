using System.Collections;
using UnityEngine;

public class MergeSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск сортировки слиянием.");

        yield return MergeSort(array, 0, array.Length - 1, visualizer, counter);

        visualizer.ShowStatus("Сортировка слиянием завершена.");
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
        visualizer.ShowStatus($"Делим массив на диапазоны [{left};{mid}] и [{mid + 1};{right}].");
        yield return SortingHelper.WaitForNextStep();

        visualizer.HighlightPseudoCodeLine(2);
        yield return SortingHelper.WaitForNextStep();
        yield return MergeSort(array, left, mid, visualizer, counter);

        visualizer.HighlightPseudoCodeLine(3);
        yield return SortingHelper.WaitForNextStep();
        yield return MergeSort(array, mid + 1, right, visualizer, counter);

        visualizer.HighlightPseudoCodeLine(4);
        visualizer.ShowStatus("Начинается слияние двух отсортированных частей.");
        yield return SortingHelper.WaitForNextStep();

        yield return Merge(array, left, mid, right, visualizer, counter);
    }

    private IEnumerator Merge(int[] array,
                              int left,
                              int mid,
                              int right,
                              IVisualizerAPI visualizer,
                              IOperationCounter counter)
    {
        int[] temp = new int[right - left + 1];

        int i = left;
        int j = mid + 1;
        int k = 0;

        visualizer.HighlightPseudoCodeLine(5);
        yield return SortingHelper.WaitForNextStep();

        while (i <= mid && j <= right)
        {
            visualizer.HighlightPseudoCodeLine(6);

            counter.IncrementComparisons();

            visualizer.ShowStatus(
                $"Сравниваем {array[i]} и {array[j]}."
            );

            yield return SortingHelper.WaitForNextStep();

            if (array[i] <= array[j])
                temp[k++] = array[i++];
            else
                temp[k++] = array[j++];
        }

        while (i <= mid)
            temp[k++] = array[i++];

        while (j <= right)
            temp[k++] = array[j++];

        for (int t = 0; t < temp.Length; t++)
        {
            visualizer.HighlightPseudoCodeLine(7);

            array[left + t] = temp[t];

            visualizer.ShowStatus(
                $"Записываем элемент {temp[t]} в позицию {left + t}."
            );

            yield return visualizer.UpdateBar(left + t, temp[t]);

            yield return SortingHelper.WaitForNextStep();
        }

        yield return null;
    }
}