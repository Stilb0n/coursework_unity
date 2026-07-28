using System.Collections;
using UnityEngine;

public class ShellSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск сортировки Шелла.");

        int n = array.Length;

        visualizer.HighlightPseudoCodeLine(0);
        yield return SortingHelper.WaitForNextStep();

        // Начальный шаг
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            visualizer.ShowStatus(
                $"Используется шаг gap = {gap}. Элементы сравниваются через этот промежуток."
            );

            visualizer.HighlightPseudoCodeLine(1);
            yield return SortingHelper.WaitForNextStep();


            for (int i = gap; i < n; i++)
            {
                int temp = array[i];
                int j = i;


                visualizer.HighlightPseudoCodeLine(2);
                visualizer.Highlight(j, j - gap);

                visualizer.ShowStatus(
                    $"Берём элемент {temp} и пытаемся вставить его в правильное место."
                );

                yield return SortingHelper.WaitForNextStep();


                while (j >= gap)
                {
                    counter.IncrementComparisons();

                    visualizer.Highlight(j - gap, j);

                    visualizer.HighlightPseudoCodeLine(3);

                    visualizer.ShowStatus(
                        $"Сравниваем {array[j - gap]} и {temp}."
                    );

                    yield return SortingHelper.WaitForNextStep();


                    if (array[j - gap] <= temp)
                        break;


                    array[j] = array[j - gap];

                    counter.IncrementSwaps();


                    visualizer.ShowStatus(
                        $"Элемент {array[j - gap]} сдвигается вправо."
                    );


                    yield return visualizer.SwapBars(j, j - gap);


                    if (AnimationSettings.Delay > 0)
                        yield return new WaitForSeconds(AnimationSettings.Delay);


                    j -= gap;
                }


                array[j] = temp;


                visualizer.HighlightPseudoCodeLine(5);

                visualizer.ShowStatus(
                    $"Элемент {temp} помещён на позицию {j}."
                );


                yield return SortingHelper.WaitForNextStep();
            }


            visualizer.ShowStatus(
                $"Шаг gap = {gap} завершён."
            );
        }


        // Финальная окраска
        for (int i = 0; i < array.Length; i++)
        {
            visualizer.MarkSorted(i);
        }


        visualizer.ShowStatus(
            "Сортировка Шелла завершена."
        );
    }
}