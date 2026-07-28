using System.Diagnostics;

public static class CountingSortBenchmark
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

            int[] count = new int[max + 1];

            for (int i = 0; i < array.Length; i++)
            {
                count[array[i]]++;
            }

            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }

            int[] output = new int[array.Length];

            for (int i = array.Length - 1; i >= 0; i--)
            {
                output[count[array[i]] - 1] = array[i];
                count[array[i]]--;

                swaps++;
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = output[i];
            }
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Counting Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }
}