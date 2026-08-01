using System.Collections;
using UnityEngine;

public class GnomeSorter : ISorter
{
    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск Gnome Sort.");

        int index = 1;

        visualizer.HighlightPseudoCodeLine(0);
        yield return SortingHelper.WaitForNextStep();

        while (index < array.Length)
        {
            visualizer.HighlightPseudoCodeLine(1);

            visualizer.Highlight(index - 1, index);

            counter.IncrementComparisons();

            visualizer.ShowStatus(
                $"Сравниваем {array[index - 1]} и {array[index]}."
            );

            yield return SortingHelper.WaitForNextStep();

            if (array[index - 1] <= array[index])
            {
                visualizer.HighlightPseudoCodeLine(2);

                visualizer.ShowStatus(
                    "Порядок верный. Переходим вперёд."
                );

                index++;

                yield return SortingHelper.WaitForNextStep();
            }
            else
            {
                visualizer.HighlightPseudoCodeLine(3);

                int temp = array[index];
                array[index] = array[index - 1];
                array[index - 1] = temp;

                counter.IncrementSwaps();

                visualizer.ShowStatus(
                    $"Меняем {array[index]} и {array[index - 1]} местами."
                );

                yield return visualizer.SwapBars(index - 1, index);

                yield return SortingHelper.WaitForNextStep();

                visualizer.HighlightPseudoCodeLine(4);

                if (index > 1)
                    index--;
                else
                    index = 1;

                yield return SortingHelper.WaitForNextStep();
            }
        }

        for (int i = 0; i < array.Length; i++)
            visualizer.MarkSorted(i);

        visualizer.HighlightPseudoCodeLine(5);

        visualizer.ShowStatus("Gnome Sort завершён.");
    }
}