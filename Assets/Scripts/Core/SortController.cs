using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class SortController : MonoBehaviour
{
    public ArrayVisualizer visualizer; // перетащить сюда объект визуализатора в Inspector!!!
    public OperationCounter counter;    
    public TextMeshProUGUI sortingTimeText;
    private float startTime;
    public TextMeshProUGUI algorithmInfoText;
    public TMP_Dropdown algorithmDropdown;
    private ISorter sorter;   // выбранный алгоритм сортировки
    private bool isSorting = false;
    private Coroutine sortingCoroutine;
    public Slider arraySizeSlider;
    public TMP_Dropdown arrayTypeDropdown;
    public TextMeshProUGUI arraySizeText;   
    private IEnumerator RunSorting()
{
    yield return StartCoroutine(sorter.Sort(values, visualizer, counter));
    float elapsed = Time.time - startTime;
    sortingTimeText.text = "Время сортировки: " + elapsed.ToString("0.00") + " с";
    isSorting = false;
    sortingCoroutine = null;
}
    private int[] values;     // массив чисел
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {    
        UpdateAlgorithmInfo();
        UpdateArraySize();
        GenerateArray();
    }

public void GenerateArray() 
    {
    if (isSorting)
    return;
    int size = (int)arraySizeSlider.value;
    values = new int[size];


switch (arrayTypeDropdown.value)
{
    case 0: // Random
        GenerateRandomArray(size);
        break;

    case 1: // Almost Sorted
        GenerateAlmostSortedArray(size);
        break;

    case 2: // Reverse Sorted
        GenerateReverseSortedArray(size);
        break;

    case 3: // Few Unique
        GenerateFewUniqueArray(size);
        break;
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

    public void UpdateArraySize()
{
    arraySizeText.text = "Размер массива: " + (int)arraySizeSlider.value;
}
private void GenerateRandomArray(int size)
{
    for (int i = 0; i < size; i++)
        values[i] = Random.Range(1, 21);
}

private void GenerateFewUniqueArray(int size)
{
    for (int i = 0; i < size; i++)
        values[i] = Random.Range(1, 6);
}

private void GenerateAlmostSortedArray(int size)
{
    for (int i = 0; i < size; i++)
        values[i] = i + 1;

    // Сделаем несколько случайных обменов
    for (int i = 0; i < size / 10; i++)
    {
        int a = Random.Range(0, size);
        int b = Random.Range(0, size);

        (values[a], values[b]) = (values[b], values[a]);
    }
}

private void GenerateReverseSortedArray(int size)
{
    for (int i = 0; i < size; i++)
        values[i] = size - i;
}

public void StartSorting()
{
        if (isSorting)
        return;

    isSorting = true;
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
    startTime = Time.time;
    sortingTimeText.text = "Время сортировки: "; 

    sortingCoroutine = StartCoroutine(RunSorting());
}

public void ResetSorting()
{
    if (sortingCoroutine != null)
    {
        StopAllCoroutines();
        sortingCoroutine = null;
    }

    isSorting = false;

    counter.ResetCounter();
    sortingTimeText.text = "Время сортировки: ";

    GenerateArray();
}
public void UpdateAlgorithmInfo()
{
    switch (algorithmDropdown.value)
    {
        case 0:
            algorithmInfoText.text =
                "Bubble Sort\n" +
                "Лучший случай: O(n)\n" +
                "Средний случай: O(n²)\n" +
                "Худший случай: O(n²)\n\n" +
                "Устойчивая: Да";
            break;

        case 1:
            algorithmInfoText.text =
                "Insertion Sort\n" +
                "Лучший случай: O(n)\n" +
                "Средний случай: O(n²)\n" +
                "Худший случай: O(n²)\n\n" +
                "Устойчивая: Да";
            break;

        case 2:
            algorithmInfoText.text =
                "Selection Sort\n" +
                "Лучший случай: O(n²)\n" +
                "Средний случай: O(n²)\n" +
                "Худший случай: O(n²)\n\n" +
                "Устойчивая: Нет";
            break;
    }
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
