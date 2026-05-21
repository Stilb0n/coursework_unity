using System.Collections;
using UnityEngine;

public class InsertionSorter : ISorter
{
public IEnumerator Sort(int[] array, IVisualizerAPI visualizer, IOperationCounter counter)
    {
        int n = array.Length;

for (int i = 1; i < n; i++)
{
    int key = array[i];
    int j = i - 1;

    visualizer.HighlightActive(i);

    while (j >= 0 && array[j] > key)
    {
        visualizer.HighlightCompare(j, j + 1);

        array[j + 1] = array[j];
        yield return visualizer.SwapBars(j, j + 1);

        yield return new WaitForSeconds(0.1f);
        visualizer.ClearHighlights();

        j--;
    }

    array[j + 1] = key;
}
    }
}