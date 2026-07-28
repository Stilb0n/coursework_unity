using System.Collections;
using UnityEngine;

public class QuickSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск быстрой сортировки.");
        yield return QuickSort(array, 0, array.Length - 1, visualizer, counter);

        visualizer.ShowStatus("Быстрая сортировка завершена. Массив полностью отсортирован.");
    }

    private IEnumerator QuickSort(int[] array,
                                  int left,
                                  int right,
                                  IVisualizerAPI visualizer,
                                  IOperationCounter counter)
    {
        visualizer.HighlightPseudoCodeLine(0);
        yield return SortingHelper.WaitForNextStep();

        if (left >= right)
            yield break;

        int i = left;
        int j = right;

        int pivotIndex = (left + right) / 2;
        int pivot = array[pivotIndex];

        visualizer.HighlightPseudoCodeLine(1);
        visualizer.Highlight(pivotIndex, pivotIndex);
        visualizer.ShowStatus($"Выбран опорный элемент: {pivot}.");

        yield return SortingHelper.WaitForNextStep();

        visualizer.HighlightPseudoCodeLine(2);
        yield return SortingHelper.WaitForNextStep();

        while (i <= j)
        {
            while (array[i] < pivot)
            {
                visualizer.Highlight(i, pivotIndex);

                counter.IncrementComparisons();

                visualizer.ShowStatus(
                    $"Элемент {array[i]} меньше опорного {pivot}. Переходим вправо."
                );

                yield return SortingHelper.WaitForNextStep();

                i++;
            }

            while (array[j] > pivot)
            {
                visualizer.Highlight(j, pivotIndex);

                counter.IncrementComparisons();

                visualizer.ShowStatus(
                    $"Элемент {array[j]} больше опорного {pivot}. Переходим влево."
                );

                yield return SortingHelper.WaitForNextStep();

                j--;
            }

            if (i <= j)
            {
                if (i != j)
                {
                    visualizer.HighlightPseudoCodeLine(3);
                    visualizer.Highlight(i, j);

                    visualizer.ShowStatus(
                        $"Меняем элементы {array[i]} и {array[j]} местами."
                    );

                    yield return SortingHelper.WaitForNextStep();

                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                    counter.IncrementSwaps();

                    yield return visualizer.SwapBars(i, j);
                }

                i++;
                j--;
            }
        }

        visualizer.HighlightPseudoCodeLine(4);
        yield return SortingHelper.WaitForNextStep();

        if (left < j)
            yield return QuickSort(array, left, j, visualizer, counter);

        visualizer.HighlightPseudoCodeLine(5);
        yield return SortingHelper.WaitForNextStep();

        if (i < right)
            yield return QuickSort(array, i, right, visualizer, counter);
    }
}