using System.Diagnostics;

public static class InsertionBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int n = array.Length;

        for (int i = 1; i < n; i++)
        {
            int j = i;

            while (j > 0)
            {
                comparisons++;

                if (array[j - 1] <= array[j])
                    break;

                int temp = array[j];
                array[j] = array[j - 1];
                array[j - 1] = temp;

                swaps++;
                j--;
            }
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Insertion Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }
}