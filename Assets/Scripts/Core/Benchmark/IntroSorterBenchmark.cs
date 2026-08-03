using System;
using System.Diagnostics;

public static class IntroSortBenchmark
{
    private const int INSERTION_THRESHOLD = 16;

    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = Stopwatch.StartNew();

        int depthLimit = 2 * (int)Math.Floor(Math.Log(array.Length, 2));

        IntroSort(array, 0, array.Length - 1, depthLimit, ref comparisons, ref swaps);

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "std::INTROsort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }

    private static void IntroSort(
        int[] array,
        int left,
        int right,
        int depthLimit,
        ref long comparisons,
        ref long swaps)
    {
        while (right - left > INSERTION_THRESHOLD)
        {
            if (depthLimit == 0)
            {
                HeapSort(array, left, right, ref comparisons, ref swaps);
                return;
            }

            depthLimit--;

            int pivot = Partition(
                array,
                left,
                right,
                ref comparisons,
                ref swaps
            );

            IntroSort(
                array,
                pivot + 1,
                right,
                depthLimit,
                ref comparisons,
                ref swaps
            );

            right = pivot - 1;
        }

        InsertionSort(array, left, right, ref comparisons, ref swaps);
    }

    private static int Partition(
        int[] array,
        int left,
        int right,
        ref long comparisons,
        ref long swaps)
    {
        int pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            comparisons++;

            if (array[j] <= pivot)
            {
                i++;

                if (i != j)
                {
                    (array[i], array[j]) = (array[j], array[i]);
                    swaps++;
                }
            }
        }

        if (i + 1 != right)
        {
            (array[i + 1], array[right]) = (array[right], array[i + 1]);
            swaps++;
        }

        return i + 1;
    }

    private static void InsertionSort(
        int[] array,
        int left,
        int right,
        ref long comparisons,
        ref long swaps)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= left)
            {
                comparisons++;

                if (array[j] <= key)
                    break;

                array[j + 1] = array[j];
                swaps++;
                j--;
            }

            array[j + 1] = key;
        }
    }

    private static void HeapSort(
        int[] array,
        int left,
        int right,
        ref long comparisons,
        ref long swaps)
    {
        int size = right - left + 1;

        for (int i = size / 2 - 1; i >= 0; i--)
            Heapify(array, size, i, left, ref comparisons, ref swaps);

        for (int i = size - 1; i > 0; i--)
        {
            (array[left], array[left + i]) = (array[left + i], array[left]);
            swaps++;

            Heapify(array, i, 0, left, ref comparisons, ref swaps);
        }
    }

    private static void Heapify(
        int[] array,
        int heapSize,
        int root,
        int offset,
        ref long comparisons,
        ref long swaps)
    {
        while (true)
        {
            int largest = root;
            int leftChild = 2 * root + 1;
            int rightChild = 2 * root + 2;

            if (leftChild < heapSize)
            {
                comparisons++;

                if (array[offset + leftChild] > array[offset + largest])
                    largest = leftChild;
            }

            if (rightChild < heapSize)
            {
                comparisons++;

                if (array[offset + rightChild] > array[offset + largest])
                    largest = rightChild;
            }

            if (largest == root)
                return;

            (array[offset + root], array[offset + largest]) =
                (array[offset + largest], array[offset + root]);

            swaps++;

            root = largest;
        }
    }
}   