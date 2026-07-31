/* using System.Collections;
using UnityEngine;

public class IntroSorter : ISorter
{
    private const int INSERTION_THRESHOLD = 16;

    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск Introsort.");

        int depthLimit = 2 * Mathf.FloorToInt(Mathf.Log(array.Length, 2));

        visualizer.HighlightPseudoCodeLine(0);

        yield return SortingHelper.WaitForNextStep();

        yield return IntroSort(array, 0, array.Length - 1,
                               depthLimit,
                               visualizer,
                               counter);

        for (int i = 0; i < array.Length; i++)
            visualizer.MarkSorted(i);

        visualizer.HighlightPseudoCodeLine(5);

        visualizer.ShowStatus("Introsort завершён.");
    }

    private IEnumerator IntroSort(int[] array,
                                  int left,
                                  int right,
                                  int depthLimit,
                                  IVisualizerAPI visualizer,
                                  IOperationCounter counter)
    {
        if (left >= right)
            yield break;

        int size = right - left + 1;

        //----------------------------------------------------
        // Маленькие диапазоны -> Insertion Sort
        //----------------------------------------------------

        if (size <= INSERTION_THRESHOLD)
        {
            visualizer.HighlightPseudoCodeLine(1);

            visualizer.ShowStatus(
                "Маленький диапазон. Используем Insertion Sort."
            );

            yield return SortingHelper.WaitForNextStep();

            yield return Insertion(array,
                                   left,
                                   right,
                                   visualizer,
                                   counter);

            yield break;
        }

        //----------------------------------------------------
        // Глубина закончилась -> Heap Sort
        //----------------------------------------------------

        if (depthLimit == 0)
        {
            visualizer.HighlightPseudoCodeLine(2);

            visualizer.ShowStatus(
                "Достигнут предел глубины. Используем Heap Sort."
            );

            yield return SortingHelper.WaitForNextStep();

            yield return Heap(array,
                              left,
                              right,
                              visualizer,
                              counter);

            yield break;
        }

        //----------------------------------------------------
        // Quick Sort
        //----------------------------------------------------

        visualizer.HighlightPseudoCodeLine(3);

        yield return SortingHelper.WaitForNextStep();

        int pivot = array[(left + right) / 2];

        int i = left;
        int j = right;

        while (i <= j)
        {
            while (array[i] < pivot)
            {
                counter.IncrementComparisons();
                i++;
            }

            while (array[j] > pivot)
            {
                counter.IncrementComparisons();
                j--;
            }

            if (i <= j)
            {
                if (i != j)
                {
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
            yield return IntroSort(array,
                                   left,
                                   j,
                                   depthLimit - 1,
                                   visualizer,
                                   counter);

        if (i < right)
            yield return IntroSort(array,
                                   i,
                                   right,
                                   depthLimit - 1,
                                   visualizer,
                                   counter);
    }
    */