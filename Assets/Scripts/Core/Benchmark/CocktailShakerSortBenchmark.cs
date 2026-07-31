using System.Diagnostics;

public static class CocktailShakerSortBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = Stopwatch.StartNew();

        int left = 0;
        int right = array.Length - 1;
        bool swapped = true;

        while (swapped)
        {
            swapped = false;

            // Слева направо
            for (int i = left; i < right; i++)
            {
                comparisons++;

                if (array[i] > array[i + 1])
                {
                    int temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;

                    swaps++;
                    swapped = true;
                }
            }

            right--;

            if (!swapped)
                break;

            swapped = false;

            // Справа налево
            for (int i = right; i > left; i--)
            {
                comparisons++;

                if (array[i - 1] > array[i])
                {
                    int temp = array[i];
                    array[i] = array[i - 1];
                    array[i - 1] = temp;

                    swaps++;
                    swapped = true;
                }
            }

            left++;
        }

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Cocktail Shaker",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }
}