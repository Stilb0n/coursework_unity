using System.Diagnostics;

public static class GnomeSortBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = Stopwatch.StartNew();

        int index = 1;

        while (index < array.Length)
        {
            comparisons++;

            if (array[index - 1] <= array[index])
            {
                index++;
            }
            else
            {
                int temp = array[index];
                array[index] = array[index - 1];
                array[index - 1] = temp;

                swaps++;

                if (index > 1)
                    index--;
                else
                    index = 1;
            }
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Gnome Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }
}