using System.Diagnostics;

public static class BitonicSortBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = Stopwatch.StartNew();

        BitonicSort(array, 0, array.Length, true, ref comparisons, ref swaps);

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Bitonic Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }

    private static void BitonicSort(int[] array,
                                    int low,
                                    int count,
                                    bool ascending,
                                    ref long comparisons,
                                    ref long swaps)
    {
        if (count <= 1)
            return;

        int k = count / 2;

        BitonicSort(array, low, k, true, ref comparisons, ref swaps);
        BitonicSort(array, low + k, k, false, ref comparisons, ref swaps);

        BitonicMerge(array, low, count, ascending, ref comparisons, ref swaps);
    }

    private static void BitonicMerge(int[] array,
                                     int low,
                                     int count,
                                     bool ascending,
                                     ref long comparisons,
                                     ref long swaps)
    {
        if (count <= 1)
            return;

        int k = count / 2;

        for (int i = low; i < low + k; i++)
        {
            comparisons++;

            if ((array[i] > array[i + k]) == ascending)
            {
                int temp = array[i];
                array[i] = array[i + k];
                array[i + k] = temp;

                swaps++;
            }
        }

        BitonicMerge(array, low, k, ascending, ref comparisons, ref swaps);
        BitonicMerge(array, low + k, k, ascending, ref comparisons, ref swaps);
    }
}