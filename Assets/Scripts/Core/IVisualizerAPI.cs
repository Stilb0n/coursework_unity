using System.Collections;

public interface IVisualizerAPI
{
    // Поменять местами два столбика
    IEnumerator SwapBars(int i, int j);

    // Подсветить два столбика (например, при сравнении)
    void Highlight(int i, int j);

    // Можно добавить метод для установки нового значения (опционально)
    // void SetValue(int index, int value);
}
