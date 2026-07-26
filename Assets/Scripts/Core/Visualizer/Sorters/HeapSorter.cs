using System.Collections;
using UnityEngine;

public class HeapSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        int n = array.Length;

        // Построение кучи
        for (int i = n / 2 - 1; i >= 0; i--)
            yield return Heapify(array, n, i, visualizer, counter);

        // Извлечение элементов
        for (int i = n - 1; i > 0; i--)
        {
            int temp = array[0];
            array[0] = array[i];
            array[i] = temp;

            counter.IncrementSwaps();

            yield return visualizer.SwapBars(0, i);

            visualizer.MarkSorted(i);

            yield return Heapify(array, i, 0, visualizer, counter);
        }

        visualizer.MarkSorted(0);
        visualizer.ShowStatus("Heap Sort завершена.");
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
            int temp = array[root];
            array[root] = array[largest];
            array[largest] = temp;

            counter.IncrementSwaps();

            yield return visualizer.SwapBars(root, largest);

            yield return Heapify(array, size, largest, visualizer, counter);
        }
    }
}