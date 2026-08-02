using System.Collections;
using UnityEngine;

public class BogoSorter : ISorter
{
    private const float MAX_TIME = 30f;

    public IEnumerator Sort(int[] array,
                            IVisualizerAPI visualizer,
                            IOperationCounter counter)
    {
        visualizer.ShowStatus("Запуск Bogo Sort.");

        float startTime = Time.time;

        while (!IsSorted(array, counter))
        {
            // Ограничение по времени
            if (Time.time - startTime >= MAX_TIME)
            {
                visualizer.ShowStatus("Прошло 30 секунд. Bogo Sort остановлен.");
                yield break;
            }

            visualizer.HighlightPseudoCodeLine(0);
            yield return SortingHelper.WaitForNextStep();

            visualizer.HighlightPseudoCodeLine(1);

            visualizer.ShowStatus("Массив не отсортирован. Перемешиваем...");

            yield return Shuffle(array, visualizer, counter);

            visualizer.HighlightPseudoCodeLine(2);

            yield return SortingHelper.WaitForNextStep();
        }

        for (int i = 0; i < array.Length; i++)
            visualizer.MarkSorted(i);

        visualizer.HighlightPseudoCodeLine(5);

        visualizer.ShowStatus("Bogo Sort завершён.");
    }

    private bool IsSorted(int[] array,
                          IOperationCounter counter)
    {
        for (int i = 1; i < array.Length; i++)
        {
            counter.IncrementComparisons();

            if (array[i - 1] > array[i])
                return false;
        }

        return true;
    }

    private IEnumerator Shuffle(int[] array,
                                IVisualizerAPI visualizer,
                                IOperationCounter counter)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            if (i != j)
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;

                counter.IncrementSwaps();

                visualizer.Highlight(i, j);

                yield return visualizer.SwapBars(i, j);

                yield return SortingHelper.WaitForNextStep();
            }
        }
    }
}