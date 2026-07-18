using System.Collections;
using UnityEngine;

public class SelectionSorter : ISorter
{
    public IEnumerator Sort(int[] array, IVisualizerAPI visualizer, IOperationCounter counter)
    {
        int n = array.Length;

        for (int i = 0; i < n - 1; i++)
        {
            while (SortingState.IsPaused)
            yield return null;
            visualizer.HighlightPseudoCodeLine(0);
            yield return new WaitForSeconds(0.2f);

            visualizer.ShowStatus(
                $"Начинаем поиск минимального элемента начиная с позиции {i}."
            );

            int minIndex = i;

            for (int j = i + 1; j < n; j++)
            {
                visualizer.HighlightPseudoCodeLine(1);
                yield return new WaitForSeconds(0.2f);

                counter.IncrementComparisons();

                visualizer.Highlight(minIndex, j);

                if (AnimationSettings.Delay > 0)
                    yield return new WaitForSeconds(AnimationSettings.Delay);

                if (array[j] < array[minIndex])
                {
                    minIndex = j;

                    visualizer.ShowStatus(
                        $"Найден новый минимальный элемент: {array[minIndex]}."
                    );
                }
            }

            if (minIndex != i)
            {
                visualizer.HighlightPseudoCodeLine(2);
                yield return new WaitForSeconds(0.2f);

                visualizer.ShowStatus(
                    $"Меняем местами элемент {array[i]} и найденный минимум {array[minIndex]}."
                );

                int temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;

                counter.IncrementSwaps();

                yield return visualizer.SwapBars(i, minIndex);
            }

            visualizer.MarkSorted(i);

            visualizer.HighlightPseudoCodeLine(3);
            yield return new WaitForSeconds(0.2f);

            visualizer.ShowStatus(
                $"Элемент на позиции {i} теперь находится на своём окончательном месте."
            );
        }

        visualizer.MarkSorted(array.Length - 1);

        visualizer.ShowStatus("Сортировка выбором завершена. Массив полностью отсортирован.");
    }
}