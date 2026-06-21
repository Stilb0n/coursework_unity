using System.Collections;

public interface IVisualizerAPI
{
    // Поменять местами два столбика
    IEnumerator SwapBars(int i, int j);

    // Подсветить два столбика (например, при сравнении)
    void Highlight(int i, int j);
    // Перекрасить отсортированные столбики.
    void MarkSorted(int index);
    // void HighlightActive(int index);
    // void HighlightCompare(int i, int j);
    // void ClearHighlights();
    // Можно добавить метод для установки нового значения (опционально)
    // void SetValue(int index, int value);
}
