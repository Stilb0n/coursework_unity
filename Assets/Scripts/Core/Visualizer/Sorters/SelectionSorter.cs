using System.Collections;
using UnityEngine;

public class SelectionSorter : ISorter
{
    public IEnumerator Sort(int[] array, IVisualizerAPI visualizer)
    {
        int n = array.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;

            // ищем минимум
            for (int j = i + 1; j < n; j++)
            {
                visualizer.Highlight(minIndex, j);
                yield return new WaitForSeconds(0.1f);

                if (array[j] < array[minIndex])
                {
                    minIndex = j;
                }
            }

            // если нашли элемент меньше — меняем
            if (minIndex != i)
            {
                int temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;

                yield return visualizer.SwapBars(i, minIndex);
            }
            visualizer.MarkSorted(i);
        }
        visualizer.MarkSorted(array.Length - 1);
    }
}
