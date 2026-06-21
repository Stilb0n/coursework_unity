public IEnumerator Sort(int[] array, IVisualizerAPI visualizer, IOperationCounter counter)
{
    int n = array.Length;

    for (int i = 1; i < n; i++)
    {
        int j = i;

        while (j > 0)
        {
            counter.IncrementComparisons();

            visualizer.Highlight(j - 1, j);

            if (array[j - 1] <= array[j])
                break;

            int temp = array[j];
            array[j] = array[j - 1];
            array[j - 1] = temp;

            counter.IncrementSwaps();

            yield return visualizer.SwapBars(j - 1, j);

            yield return new WaitForSeconds(0.1f);

            j--;
        }
    }
}