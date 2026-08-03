using System.Collections;
using UnityEngine;

public class BitonicSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск Bitonic Sort.");

        yield return BitonicSort(array, 0, array.Length, true, visualizer, counter);

        for (int i = 0; i < array.Length; i++)
            visualizer.MarkSorted(i);

        visualizer.HighlightPseudoCodeLine(5);

        visualizer.ShowStatus("Bitonic Sort завершён.");
    }

    private IEnumerator BitonicSort(int[] array,
                                    int low,
                                    int cnt,
                                    bool ascending,
                                    IVisualizerAPI visualizer,
                                    IOperationCounter counter)
    {
        if (cnt <= 1)
            yield break;

        int k = cnt / 2;

        visualizer.HighlightPseudoCodeLine(0);

        yield return SortingHelper.WaitForNextStep();

        yield return BitonicSort(array,
                                 low,
                                 k,
                                 true,
                                 visualizer,
                                 counter);

        yield return BitonicSort(array,
                                 low + k,
                                 k,
                                 false,
                                 visualizer,
                                 counter);

        visualizer.HighlightPseudoCodeLine(1);

        yield return SortingHelper.WaitForNextStep();

        yield return BitonicMerge(array,
                                  low,
                                  cnt,
                                  ascending,
                                  visualizer,
                                  counter);
    }

    private IEnumerator BitonicMerge(int[] array,
                                     int low,
                                     int cnt,
                                     bool ascending,
                                     IVisualizerAPI visualizer,
                                     IOperationCounter counter)
    {
        if (cnt <= 1)
            yield break;

        int k = cnt / 2;

        for (int i = low; i < low + k; i++)
        {
            visualizer.HighlightPseudoCodeLine(2);

            visualizer.Highlight(i, i + k);

            counter.IncrementComparisons();

            visualizer.ShowStatus(
                $"Сравниваем {array[i]} и {array[i + k]}."
            );

            yield return SortingHelper.WaitForNextStep();

            if ((array[i] > array[i + k]) == ascending)
            {
                visualizer.HighlightPseudoCodeLine(3);

                int temp = array[i];
                array[i] = array[i + k];
                array[i + k] = temp;

                counter.IncrementSwaps();

                yield return visualizer.SwapBars(i, i + k);

                yield return SortingHelper.WaitForNextStep();
            }
        }

        visualizer.HighlightPseudoCodeLine(4);

        yield return SortingHelper.WaitForNextStep();

        yield return BitonicMerge(array,
                                  low,
                                  k,
                                  ascending,
                                  visualizer,
                                  counter);

        yield return BitonicMerge(array,
                                  low + k,
                                  k,
                                  ascending,
                                  visualizer,
                                  counter);
    }
}