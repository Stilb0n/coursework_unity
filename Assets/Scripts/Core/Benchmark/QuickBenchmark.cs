using System.Diagnostics;

public static class QuickBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        QuickSort(array, 0, array.Length - 1, ref comparisons, ref swaps);

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Quick Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }

    private static void QuickSort(int[] array,
                                  int left,
                                  int right,
                                  ref long comparisons,
                                  ref long swaps)
    {
        if (left >= right)
            return;

        int i = left;
        int j = right;
        int pivot = array[(left + right) / 2];

        while (i <= j)
        {
            while (true)
            {
                comparisons++;

                if (!(array[i] < pivot))
                    break;

                i++;
            }

            while (true)
            {
                comparisons++;

                if (!(array[j] > pivot))
                    break;

                j--;
            }

            if (i <= j)
            {
                if (i != j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                    swaps++;
                }

                i++;
                j--;
            }
        }

        if (left < j)
            QuickSort(array, left, j, ref comparisons, ref swaps);

        if (i < right)
            QuickSort(array, i, right, ref comparisons, ref swaps);
    }
}