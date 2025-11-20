using UnityEngine;

public class SortController : MonoBehaviour
{
    public ArrayVisualizer visualizer; // перетащи сюда объект визуализатора в Inspector

    private ISorter sorter;   // выбранный алгоритм сортировки
    private int[] values;     // массив чисел
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int size = 20;
        values = new int[size];

    // Генерация случайных чисел от 1 до 20
    for (int i = 0; i < size; i++)
    {
        values[i] = Random.Range(1, 21); // верхняя граница не включается
    }
if (visualizer == null)
{
    Debug.LogError("Visualizer не назначен!");
    return;
}

if (visualizer.barPrefab == null)
{
    Debug.LogError("barPrefab не назначен!");
    return;
}
    visualizer.CreateBars(values);

    sorter = new BubbleSorter();
    StartCoroutine(sorter.Sort(values, visualizer));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
