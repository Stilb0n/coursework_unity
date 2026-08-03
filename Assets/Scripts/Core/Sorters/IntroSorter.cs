using System;
using System.Collections;
using UnityEngine;

public class IntroSorter : ISorter
{
    private const int INSERTION_THRESHOLD = 16;

    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск Introsort.");

        int depthLimit = 2 * (int)Math.Floor(Math.Log(array.Length, 2));

        yield return IntroSort(
            array,
            0,
            array.Length - 1,
            depthLimit,
            visualizer,
            counter
        );

        for (int i = 0; i < array.Length; i++)
            visualizer.MarkSorted(i);

        visualizer.HighlightPseudoCodeLine(5);
        visualizer.ShowStatus("Introsort завершён.");
    }

    private IEnumerator IntroSort(
        int[] array,
        int left,
        int right,
        int depthLimit,
        IVisualizerAPI visualizer,
        IOperationCounter counter)
    {
        while (right - left > INSERTION_THRESHOLD)
        {
            if (depthLimit == 0)
            {
                visualizer.HighlightPseudoCodeLine(2);
                visualizer.ShowStatus("Переходим на Heap Sort.");

                yield return HeapSort(
                    array,
                    left,
                    right,
                    visualizer,
                    counter
                );

                yield break;
            }

            depthLimit--;

            visualizer.HighlightPseudoCodeLine(0);

            int pivot = 0;

            yield return Partition(
                array,
                left,
                right,
                visualizer,
                counter,
                p => pivot = p
            );

            yield return IntroSort(
                array,
                pivot + 1,
                right,
                depthLimit,
                visualizer,
                counter
            );

            right = pivot - 1;
        }

        visualizer.HighlightPseudoCodeLine(3);

        yield return InsertionSort(
            array,
            left,
            right,
            visualizer,
            counter
        );
    }    private IEnumerator Partition(
        int[] array,
        int left,
        int right,
        IVisualizerAPI visualizer,
        IOperationCounter counter,
        Action<int> setPivot)
    {
        int pivot = array[right];
        int i = left - 1;

        visualizer.HighlightPseudoCodeLine(1);

        for (int j = left; j < right; j++)
        {
            visualizer.Highlight(j, right);

            counter.IncrementComparisons();

            yield return SortingHelper.WaitForNextStep();

            if (array[j] <= pivot)
            {
                i++;

                if (i != j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                    counter.IncrementSwaps();

                    yield return visualizer.SwapBars(i, j);

                    yield return SortingHelper.WaitForNextStep();
                }
            }
        }

        if (i + 1 != right)
        {
            int temp = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp;

            counter.IncrementSwaps();

            yield return visualizer.SwapBars(i + 1, right);

            yield return SortingHelper.WaitForNextStep();
        }

        setPivot(i + 1);
    }

    private IEnumerator InsertionSort(
        int[] array,
        int left,
        int right,
        IVisualizerAPI visualizer,
        IOperationCounter counter)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= left)
            {
                counter.IncrementComparisons();

                visualizer.Highlight(j, j + 1);

                yield return SortingHelper.WaitForNextStep();

                if (array[j] <= key)
                    break;

                array[j + 1] = array[j];

                counter.IncrementSwaps();

                yield return visualizer.UpdateBar(j + 1, array[j]);

                j--;
            }

            array[j + 1] = key;

            yield return visualizer.UpdateBar(j + 1, key);
        }
    }    private IEnumerator HeapSort(
        int[] array,
        int left,
        int right,
        IVisualizerAPI visualizer,
        IOperationCounter counter)
    {
        int size = right - left + 1;

        // Построение кучи
        for (int i = size / 2 - 1; i >= 0; i--)
        {
            yield return Heapify(
                array,
                size,
                i,
                left,
                visualizer,
                counter
            );
        }

        // Извлечение максимума
        for (int i = size - 1; i > 0; i--)
        {
            int temp = array[left];
            array[left] = array[left + i];
            array[left + i] = temp;

            counter.IncrementSwaps();

            yield return visualizer.SwapBars(left, left + i);

            yield return SortingHelper.WaitForNextStep();

            yield return Heapify(
                array,
                i,
                0,
                left,
                visualizer,
                counter
            );
        }
    }

    private IEnumerator Heapify(
        int[] array,
        int heapSize,
        int root,
        int offset,
        IVisualizerAPI visualizer,
        IOperationCounter counter)
    {
        while (true)
        {
            int largest = root;
            int leftChild = 2 * root + 1;
            int rightChild = 2 * root + 2;

            if (leftChild < heapSize)
            {
                counter.IncrementComparisons();

                if (array[offset + leftChild] > array[offset + largest])
                    largest = leftChild;
            }

            if (rightChild < heapSize)
            {
                counter.IncrementComparisons();

                if (array[offset + rightChild] > array[offset + largest])
                    largest = rightChild;
            }

            if (largest == root)
                yield break;

            visualizer.Highlight(offset + root, offset + largest);

            int temp = array[offset + root];
            array[offset + root] = array[offset + largest];
            array[offset + largest] = temp;

            counter.IncrementSwaps();

            yield return visualizer.SwapBars(
                offset + root,
                offset + largest
            );

            yield return SortingHelper.WaitForNextStep();

            root = largest;
        }
    }
}