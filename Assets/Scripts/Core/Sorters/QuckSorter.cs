using System.Collections;
using UnityEngine;

// Реализация быстрой сортировки
public class QuickSorter : ISorter
{
    private IEnumerator Wait()
    {
        yield return SortingHelper.WaitForNextStep();

        if (AnimationSettings.Delay > 0f)
            yield return new WaitForSeconds(AnimationSettings.Delay);
        else
            yield return null;
    }

    public IEnumerator Sort(
        int[] array,
        IVisualizerAPI visualizer,
        IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск быстрой сортировки.");

        yield return QuickSort(
            array,
            0,
            array.Length - 1,
            visualizer,
            counter
        );

        visualizer.ShowStatus(
            "Быстрая сортировка завершена. Массив полностью отсортирован."
        );
    }

    private IEnumerator QuickSort(
        int[] array,
        int left,
        int right,
        IVisualizerAPI visualizer,
        IOperationCounter counter)
    {
        visualizer.HighlightPseudoCodeLine(0);
        yield return Wait();

        if (left >= right)
            yield break;

        int i = left;
        int j = right;

        int pivotIndex = (left + right) / 2;
        int pivot = array[pivotIndex];

        visualizer.HighlightPseudoCodeLine(1);
        visualizer.Highlight(pivotIndex, pivotIndex);

        visualizer.ShowStatus(
            $"Выбран опорный элемент: {pivot}."
        );

        yield return Wait();

        visualizer.HighlightPseudoCodeLine(2);
        yield return Wait();

        while (i <= j)
        {
            // Поиск элемента слева
            while (array[i] < pivot)
            {
                counter.IncrementComparisons();

                visualizer.Highlight(i, pivotIndex);

                visualizer.ShowStatus(
                    $"Элемент {array[i]} меньше опорного {pivot}. Переходим вправо."
                );

                yield return Wait();

                i++;

                // Защита от выхода за границы
                if (i > right)
                    break;
            }

            // Поиск элемента справа
            while (array[j] > pivot)
            {
                counter.IncrementComparisons();

                visualizer.Highlight(j, pivotIndex);

                visualizer.ShowStatus(
                    $"Элемент {array[j]} больше опорного {pivot}. Переходим влево."
                );

                yield return Wait();

                j--;

                // Защита от выхода за границы
                if (j < left)
                    break;
            }

            if (i <= j)
            {
                visualizer.HighlightPseudoCodeLine(3);
                visualizer.Highlight(i, j);

                if (i != j)
                {
                    visualizer.ShowStatus(
                        $"Меняем элементы {array[i]} и {array[j]} местами."
                    );

                    yield return Wait();

                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                    counter.IncrementSwaps();

                    yield return visualizer.SwapBars(i, j);

                    yield return Wait();
                }

                i++;
                j--;
            }
        }

        visualizer.HighlightPseudoCodeLine(4);
        yield return Wait();

        // Левая часть
        if (left < j)
        {
            yield return QuickSort(
                array,
                left,
                j,
                visualizer,
                counter
            );
        }

        // Правая часть
        visualizer.HighlightPseudoCodeLine(5);
        yield return Wait();

        if (i < right)
        {
            yield return QuickSort(
                array,
                i,
                right,
                visualizer,
                counter
            );
        }
    }
}