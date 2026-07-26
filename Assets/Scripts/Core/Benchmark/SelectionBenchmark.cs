using System.Diagnostics;

public static class SelectionBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int n = array.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < n; j++)
            {
                comparisons++;

                if (array[j] < array[minIndex])
                    minIndex = j;
            }

            if (minIndex != i)
            {
                int temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;

                swaps++;
            }
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Selection Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }
}