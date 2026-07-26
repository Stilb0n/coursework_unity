using System.Diagnostics;

public static class HeapBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int n = array.Length;

        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(array, n, i, ref comparisons, ref swaps);

        for (int i = n - 1; i > 0; i--)
        {
            int temp = array[0];
            array[0] = array[i];
            array[i] = temp;

            swaps++;

            Heapify(array, i, 0, ref comparisons, ref swaps);
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Heap Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }

    private static void Heapify(int[] array,
                                int size,
                                int root,
                                ref long comparisons,
                                ref long swaps)
    {
        int largest = root;

        int left = 2 * root + 1;
        int right = 2 * root + 2;

        if (left < size)
        {
            comparisons++;

            if (array[left] > array[largest])
                largest = left;
        }

        if (right < size)
        {
            comparisons++;

            if (array[right] > array[largest])
                largest = right;
        }

        if (largest != root)
        {
            int temp = array[root];
            array[root] = array[largest];
            array[largest] = temp;

            swaps++;

            Heapify(array,
                    size,
                    largest,
                    ref comparisons,
                    ref swaps);
        }
    }
}