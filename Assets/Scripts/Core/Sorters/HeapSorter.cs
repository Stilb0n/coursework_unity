using System.Collections;
using UnityEngine;

public class HeapSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        int n = array.Length;

        visualizer.ShowStatus("Построение максимальной кучи.");

        visualizer.HighlightPseudoCodeLine(0);
        yield return SortingHelper.WaitForNextStep();

        // строим кучу
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            yield return Heapify(array, n, i, visualizer, counter);
        }


        visualizer.ShowStatus("Куча построена. Начинаем сортировку.");


        for (int i = n - 1; i > 0; i--)
        {
            visualizer.HighlightPseudoCodeLine(4);
            visualizer.ShowStatus(
                $"Перемещаем максимальный элемент {array[0]} в конец."
            );

            yield return SortingHelper.WaitForNextStep();


            int temp = array[0];
            array[0] = array[i];
            array[i] = temp;

            counter.IncrementSwaps();

            yield return visualizer.SwapBars(0, i);

            visualizer.MarkSorted(i);


            visualizer.HighlightPseudoCodeLine(5);
            yield return SortingHelper.WaitForNextStep();


            yield return Heapify(array, i, 0, visualizer, counter);
        }


        visualizer.MarkSorted(0);

        visualizer.ShowStatus(
            "Heap Sort завершена. Массив полностью отсортирован."
        );
    }



    private IEnumerator Heapify(int[] array,
                                int size,
                                int root,
                                IVisualizerAPI visualizer,
                                IOperationCounter counter)
    {
        int largest = root;

        int left = 2 * root + 1;
        int right = 2 * root + 2;


        visualizer.HighlightPseudoCodeLine(1);

        visualizer.HighlightHeap(root, left, right);

        yield return SortingHelper.WaitForNextStep();



        if (left < size)
        {
            counter.IncrementComparisons();

            if (array[left] > array[largest])
                largest = left;
        }



        if (right < size)
        {
            counter.IncrementComparisons();

            if (array[right] > array[largest])
                largest = right;
        }



        if (largest != root)
        {
            visualizer.HighlightPseudoCodeLine(2);

            visualizer.ShowStatus(
                $"Меняем {array[root]} и {array[largest]}."
            );

            yield return SortingHelper.WaitForNextStep();



            int temp = array[root];
            array[root] = array[largest];
            array[largest] = temp;


            counter.IncrementSwaps();


            yield return visualizer.SwapBars(root, largest);



            visualizer.HighlightPseudoCodeLine(3);

            yield return SortingHelper.WaitForNextStep();


            yield return Heapify(
                array,
                size,
                largest,
                visualizer,
                counter
            );
        }
    }
}