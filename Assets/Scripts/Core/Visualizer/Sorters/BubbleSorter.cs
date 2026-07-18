using System.Collections;
using UnityEngine;

// Реализация пузырьковой сортировки
public class BubbleSorter : ISorter
{
    private IEnumerator WaitIfPaused()
    {
        while (SortingState.IsPaused)
            yield return null;
    }

    public IEnumerator Sort(int[] array, IVisualizerAPI visualizer, IOperationCounter counter)
    {
        int n = array.Length;

        for (int i = 0; i < n - 1; i++)
        {
            yield return WaitIfPaused();

            visualizer.HighlightPseudoCodeLine(0);
            yield return new WaitForSeconds(0.2f);

            visualizer.ShowStatus(
                $"Начался проход {i + 1} из {n - 1}. Самый большой элемент постепенно перемещается в конец массива."
            );

            for (int j = 0; j < n - i - 1; j++)
            {
                yield return WaitIfPaused();

                visualizer.HighlightPseudoCodeLine(1);
                yield return new WaitForSeconds(0.2f);

                counter.IncrementComparisons();

                visualizer.Highlight(j, j + 1);

                visualizer.HighlightPseudoCodeLine(2);
                yield return new WaitForSeconds(0.2f);

                if (array[j] > array[j + 1])
                {
                    visualizer.ShowStatus(
                        $"Элемент {array[j]} больше элемента {array[j + 1]}, поэтому они меняются местами."
                    );

                    visualizer.HighlightPseudoCodeLine(3);
                    yield return new WaitForSeconds(0.2f);

                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;

                    counter.IncrementSwaps();

                    yield return WaitIfPaused();
                    yield return visualizer.SwapBars(j, j + 1);
                }
                else
                {
                    visualizer.ShowStatus(
                        $"Элемент {array[j]} меньше или равен элементу {array[j + 1]}, поэтому обмен не требуется."
                    );
                }

                yield return WaitIfPaused();

                if (AnimationSettings.Delay > 0)
                    yield return new WaitForSeconds(AnimationSettings.Delay);
            }

            visualizer.ShowStatus(
                $"Проход {i + 1} завершён. Последний элемент не будет участвовать в следующих проходах."
            );
        }

        visualizer.ShowStatus(
            "Пузырьковая сортировка завершена. Массив полностью отсортирован."
        );
    }
}