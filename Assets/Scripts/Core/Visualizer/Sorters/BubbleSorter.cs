using System.Collections;
using UnityEngine;



// Реализация пузырьковой сортировки
public class BubbleSorter : ISorter
{
    public IEnumerator Sort(int[] array, IVisualizerAPI visualizer, IOperationCounter counter)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                counter.IncrementComparisons();
                // Подсветка сравниваемых элементов
                visualizer.Highlight(j, j + 1);

                // Если нужно поменять местами
                if (array[j] > array[j + 1])
                {
                    // Меняем данные в массиве
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                    counter.IncrementSwaps();
                    // Анимация swap
                    yield return visualizer.SwapBars(j, j + 1);
                }

                // Небольшая пауза, чтобы видеть шаг
               if (AnimationSettings.Delay > 0)
                yield return new WaitForSeconds(AnimationSettings.Delay);
            }
        }
    }
}
