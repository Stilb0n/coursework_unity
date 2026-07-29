using System.Diagnostics;

public static class RadixSortBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = Stopwatch.StartNew();

        if (array.Length > 0)
        {
            int max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                comparisons++;

                if (array[i] > max)
                    max = array[i];
            }

            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSort(array, exp, ref swaps);
            }
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Radix Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }

    private static void CountingSort(int[] array,
                                     int exp,
                                     ref long swaps)
    {
        int n = array.Length;

        int[] output = new int[n];
        int[] count = new int[10];

        for (int i = 0; i < n; i++)
        {
            count[(array[i] / exp) % 10]++;
        }

        for (int i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
        }

        for (int i = n - 1; i >= 0; i--)
        {
            int digit = (array[i] / exp) % 10;

            output[count[digit] - 1] = array[i];

            count[digit]--;

            swaps++;
        }

        for (int i = 0; i < n; i++)
        {
            array[i] = output[i];
        }
    }
}