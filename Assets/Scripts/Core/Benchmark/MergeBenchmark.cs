using System.Diagnostics;

public static class MergeBenchmark
{
    public static SortBenchmarkResult Run(int[] originalArray)
    {
        int[] array = (int[])originalArray.Clone();

        long comparisons = 0;
        long swaps = 0;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        MergeSort(array, 0, array.Length - 1, ref comparisons, ref swaps);

        stopwatch.Stop();

        return new SortBenchmarkResult
        {
            AlgorithmName = "Merge Sort",
            Comparisons = comparisons,
            Swaps = swaps,
            TimeMs = (float)stopwatch.Elapsed.TotalMilliseconds
        };
    }

    private static void MergeSort(int[] array,
                                  int left,
                                  int right,
                                  ref long comparisons,
                                  ref long swaps)
    {
        if (left >= right)
            return;

        int mid = (left + right) / 2;

        MergeSort(array, left, mid, ref comparisons, ref swaps);
        MergeSort(array, mid + 1, right, ref comparisons, ref swaps);

        Merge(array, left, mid, right, ref comparisons, ref swaps);
    }

    private static void Merge(int[] array,
                              int left,
                              int mid,
                              int right,
                              ref long comparisons,
                              ref long swaps)
    {
        int[] temp = new int[right - left + 1];

        int i = left;
        int j = mid + 1;
        int k = 0;

        while (i <= mid && j <= right)
        {
            comparisons++;

            if (array[i] <= array[j])
                temp[k++] = array[i++];
            else
                temp[k++] = array[j++];
        }

        while (i <= mid)
            temp[k++] = array[i++];

        while (j <= right)
            temp[k++] = array[j++];

        for (int t = 0; t < temp.Length; t++)
        {
            array[left + t] = temp[t];
            swaps++;
        }
    }
}