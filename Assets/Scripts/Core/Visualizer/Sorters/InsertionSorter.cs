using System.Collections;
using UnityEngine;

public class InsertionSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        int n = array.Length;

        for (int i = 1; i < n; i++)
        {
            visualizer.HighlightPseudoCodeLine(0);
            yield return new WaitForSeconds(0.2f);

            visualizer.ShowStatus(
                $"Начинаем вставку элемента {array[i]} в уже отсортированную часть массива."
            );

            int j = i;

            visualizer.HighlightPseudoCodeLine(1);
            yield return new WaitForSeconds(0.2f);

            while (j > 0)
            {
                visualizer.HighlightPseudoCodeLine(2);
                yield return new WaitForSeconds(0.2f);

                counter.IncrementComparisons();

                visualizer.Highlight(j - 1, j);

                if (array[j - 1] <= array[j])
                {
                    visualizer.ShowStatus(
                        $"Элемент {array[j]} уже находится на правильном месте."
                    );
                    break;
                }

                visualizer.ShowStatus(
                    $"Элемент {array[j]} меньше элемента {array[j - 1]}, выполняется обмен."
                );

                visualizer.HighlightPseudoCodeLine(3);
                yield return new WaitForSeconds(0.2f);

                int temp = array[j];
                array[j] = array[j - 1];
                array[j - 1] = temp;

                counter.IncrementSwaps();

                yield return visualizer.SwapBars(j - 1, j);

                if (AnimationSettings.Delay > 0)
                    yield return new WaitForSeconds(AnimationSettings.Delay);

                j--;
            }

            visualizer.ShowStatus("Вставка элемента завершена.");
        }

        visualizer.ShowStatus("Сортировка вставками завершена. Массив полностью отсортирован.");
    }
}