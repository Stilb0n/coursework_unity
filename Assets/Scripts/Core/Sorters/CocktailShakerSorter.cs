using System.Collections;
using UnityEngine;

public class CocktailShakerSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск Cocktail Shaker Sort.");

        int left = 0;
        int right = array.Length - 1;
        bool swapped = true;

        while (swapped)
        {
            swapped = false;

            // Проход слева направо
            visualizer.HighlightPseudoCodeLine(0);
            visualizer.ShowStatus("Проход слева направо.");
            yield return SortingHelper.WaitForNextStep();

            for (int i = left; i < right; i++)
            {
                visualizer.HighlightPseudoCodeLine(1);
                visualizer.Highlight(i, i + 1);

                counter.IncrementComparisons();

                visualizer.ShowStatus(
                    $"Сравниваем {array[i]} и {array[i + 1]}."
                );

                yield return SortingHelper.WaitForNextStep();

                if (array[i] > array[i + 1])
                {
                    visualizer.HighlightPseudoCodeLine(2);

                    int temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;

                    counter.IncrementSwaps();

                    yield return visualizer.SwapBars(i, i + 1);

                    swapped = true;

                    visualizer.ShowStatus("Выполняем обмен.");

                    yield return SortingHelper.WaitForNextStep();
                }
            }

            visualizer.MarkSorted(right);
            right--;

            if (!swapped)
                break;

            swapped = false;

            // Проход справа налево
            visualizer.HighlightPseudoCodeLine(3);
            visualizer.ShowStatus("Проход справа налево.");
            yield return SortingHelper.WaitForNextStep();

            for (int i = right; i > left; i--)
            {
                visualizer.HighlightPseudoCodeLine(1);
                visualizer.Highlight(i - 1, i);

                counter.IncrementComparisons();

                visualizer.ShowStatus(
                    $"Сравниваем {array[i - 1]} и {array[i]}."
                );

                yield return SortingHelper.WaitForNextStep();

                if (array[i - 1] > array[i])
                {
                    visualizer.HighlightPseudoCodeLine(2);

                    int temp = array[i];
                    array[i] = array[i - 1];
                    array[i - 1] = temp;

                    counter.IncrementSwaps();

                    yield return visualizer.SwapBars(i - 1, i);

                    swapped = true;

                    visualizer.ShowStatus("Выполняем обмен.");

                    yield return SortingHelper.WaitForNextStep();
                }
            }

            visualizer.MarkSorted(left);
            left++;

            visualizer.HighlightPseudoCodeLine(4);
            yield return SortingHelper.WaitForNextStep();
        }

        for (int i = left; i <= right; i++)
            visualizer.MarkSorted(i);

        visualizer.HighlightPseudoCodeLine(5);
        visualizer.ShowStatus("Cocktail Shaker Sort завершён.");
    }
}