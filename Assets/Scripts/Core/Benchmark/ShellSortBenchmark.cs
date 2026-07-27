using System.Diagnostics;

public static class ShellSortBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;


        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();


        int n = array.Length;


        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = array[i];

                int j = i;


                while (j >= gap)
                {
                    comparisons++;


                    if (array[j - gap] <= temp)
                        break;


                    array[j] = array[j - gap];

                    swaps++;

                    j -= gap;
                }


                array[j] = temp;
            }
        }


        stopwatch.Stop();


        return new SortBenchmarkResult
        {
            AlgorithmName = "Shell Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }
}