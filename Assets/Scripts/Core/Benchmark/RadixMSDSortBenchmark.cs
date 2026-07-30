using System.Collections.Generic;
using System.Diagnostics;

public static class RadixMSDSortBenchmark
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

            int exp = 1;

            while (max / exp >= 10)
                exp *= 10;

            MSDSort(array, 0, array.Length - 1, exp, ref swaps);
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Radix Sort (MSD)",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }

    private static void MSDSort(int[] array,
                                int left,
                                int right,
                                int exp,
                                ref long swaps)
    {
        if (left >= right || exp == 0)
            return;

        List<int>[] buckets = new List<int>[10];

        for (int i = 0; i < 10; i++)
            buckets[i] = new List<int>();

        for (int i = left; i <= right; i++)
        {
            int digit = (array[i] / exp) % 10;
            buckets[digit].Add(array[i]);
            swaps++;
        }

        int index = left;

        int[] bucketStart = new int[10];
        int[] bucketEnd = new int[10];

        for (int b = 0; b < 10; b++)
        {
            bucketStart[b] = index;

            foreach (int value in buckets[b])
            {
                array[index++] = value;
            }

            bucketEnd[b] = index - 1;
        }

        if (exp == 1)
            return;

        for (int b = 0; b < 10; b++)
        {
            if (bucketStart[b] < bucketEnd[b])
            {
                MSDSort(
                    array,
                    bucketStart[b],
                    bucketEnd[b],
                    exp / 10,
                    ref swaps
                );
            }
        }
    }
}