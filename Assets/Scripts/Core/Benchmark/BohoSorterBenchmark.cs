using System.Diagnostics;

public static class BogoSortBenchmark
{
    private const double MAX_TIME_SECONDS = 30.0;

    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = Stopwatch.StartNew();

        while (!IsSorted(array, ref comparisons))
        {
            if (stopwatch.Elapsed.TotalSeconds >= MAX_TIME_SECONDS)
                break;

            Shuffle(array, ref swaps);
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = stopwatch.Elapsed.TotalSeconds >= MAX_TIME_SECONDS
    ? "Bogo Sort (Timeout)"
    : "Bogo Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }

    private static bool IsSorted(int[] array, ref long comparisons)
    {
        for (int i = 1; i < array.Length; i++)
        {
            comparisons++;

            if (array[i - 1] > array[i])
                return false;
        }

        return true;
    }

    private static void Shuffle(int[] array, ref long swaps)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);

            if (i != j)
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;

                swaps++;
            }
        }
    }
}