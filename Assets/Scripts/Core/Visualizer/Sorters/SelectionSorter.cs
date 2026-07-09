using System.Collections;
using UnityEngine;

public class SelectionSorter : ISorter
{
    public IEnumerator Sort(int[] array, IVisualizerAPI visualizer, IOperationCounter counter)
    {
        int n = array.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;	    
            // ищем минимум
            for (int j = i + 1; j < n; j++)
            {
                counter.IncrementComparisons();
		visualizer.Highlight(minIndex, j);
                if (AnimationSettings.Delay > 0)
                 yield return new WaitForSeconds(AnimationSettings.Delay);   

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
		
		counter.IncrementSwaps();

                yield return visualizer.SwapBars(i, minIndex);
            }
            visualizer.MarkSorted(i);
        }
        visualizer.MarkSorted(array.Length - 1);
    }
}
