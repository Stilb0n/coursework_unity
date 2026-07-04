using UnityEngine;
using TMPro;
public class SortController : MonoBehaviour
{
    public ArrayVisualizer visualizer; // перетащить сюда объект визуализатора в Inspector!!!
    public OperationCounter counter;    
    public TMP_Dropdown algorithmDropdown;
    private ISorter sorter;   // выбранный алгоритм сортировки
    private int[] values;     // массив чисел
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {    
        GenerateArray();
    }

public void GenerateArray() 
    {
    int size = 20;
    values = new int[size];

    for (int i = 0; i < size; i++)
    {
        values[i] = Random.Range(1, 21);
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
    }
public void StartSorting()
{
    switch (algorithmDropdown.value)
    {
        case 0:
            Debug.Log("Выбран Bubble Sort");
            sorter = new BubbleSorter();
            break;

        case 1:
             Debug.Log("Выбран InsertionSorter");
            sorter = new InsertionSorter();
            break;
        case 2:
            Debug.Log("Выбран SelectionSorter");
            sorter = new SelectionSorter();
            break;
    }

    counter.ResetCounter();

    StartCoroutine(sorter.Sort(values, visualizer, counter));
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
