using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class SortController : MonoBehaviour
{
    public ArrayVisualizer visualizer; // перетащить сюда объект визуализатора в Inspector!!!
    public OperationCounter counter;  
    public BenchmarkUI benchmarkUI;
    [SerializeField] private int benchmarkArraySize = 10000;    
    public TextMeshProUGUI sortingTimeText;
    private float startTime;
    public TextMeshProUGUI algorithmInfoText;
    public TMP_Dropdown algorithmDropdown;
    private ISorter sorter;   // выбранный алгоритм сортировки
    private bool isSorting = false;
    private Coroutine sortingCoroutine;
    public Slider arraySizeSlider;
    public TextMeshProUGUI statusText;
    public TMP_Dropdown arrayTypeDropdown;
    public TextMeshProUGUI arraySizeText;   
public void StepForward()
{
    if (SortingState.IsPaused)
        SortingState.StepRequested = true;
}
public void CompareAlgorithms()
{
    int[] benchmarkArray = GenerateBenchmarkArray(10000);

    BenchmarkManager manager = new BenchmarkManager();

    var results = manager.RunAll(benchmarkArray);

    benchmarkUI.ShowResults(results);
}
public void TogglePause()
{
    SortingState.IsPaused = !SortingState.IsPaused;
}
    public void SetStatus(string message)
{
    statusText.text = message;
}
    private IEnumerator RunSorting()
{
    yield return StartCoroutine(sorter.Sort(values, visualizer, counter));
    float elapsed = Time.time - startTime;
    sortingTimeText.text = "Время сортировки: " + elapsed.ToString("0.00") + " с";
    isSorting = false;
    sortingCoroutine = null;
}
    private int[] values;     // массив чисел
    private int[] originalValues;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {    
        UpdateAlgorithmInfo();
        UpdateArraySize();
        GenerateArray();
        visualizer.HighlightPseudoCodeLine(5);
    }
private int[] GenerateBenchmarkArray(int size)
{
    int[] array = new int[size];

    switch (arrayTypeDropdown.value)
    {
        case 0:
            for (int i = 0; i < size; i++)
                array[i] = Random.Range(1, size + 1);
            break;

        case 1:
            for (int i = 0; i < size; i++)
                array[i] = i + 1;

            for (int i = 0; i < size / 10; i++)
            {
                int a = Random.Range(0, size);
                int b = Random.Range(0, size);

                (array[a], array[b]) = (array[b], array[a]);
            }
            break;

        case 2:
            for (int i = 0; i < size; i++)
                array[i] = size - i;
            break;

        case 3:
            for (int i = 0; i < size; i++)
                array[i] = Random.Range(1, 6);
            break;
    }

    return array;
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
    originalValues = (int[])values.Clone();
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
if (algorithmDropdown.value == 12 && values.Length > 10)
{
    visualizer.ShowStatus("Bogo Sort доступен только для массивов до 10 элементов.");
    Debug.LogWarning("Bogo Sort доступен только для массивов до 10 элементов.");
    return;
}
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
        case 3:
           Debug.Log("Выбран Quick Sort");
          sorter = new QuickSorter();
            break;
        case 4:
            Debug.Log("Выбран Merge Sort");
          sorter = new MergeSorter();
            break;
            case 5:
                        Debug.Log("Выбран Heap Sort");
             sorter = new HeapSorter();
                 break;
                 case 6:
                                         Debug.Log("Выбран ShellSorter ");
    sorter = new ShellSorter();
    break;
    case 7:
            Debug.Log("Выбран CountingSorter");
            sorter = new CountingSorter();
            break;case 8:
            Debug.Log("Выбран RadixSorter");
            sorter = new RadixSorter();
            break;case 9:
            Debug.Log("Выбран RadixSorterMSD");
            sorter = new RadixMSDSorter();
            break;case 10:
    Debug.Log("Выбран Cocktail Shaker Sort");
    sorter = new CocktailShakerSorter();
    break;case 11:
    Debug.Log("Выбран Gnome Sort");
    sorter = new GnomeSorter();
    break;case 12:
    Debug.Log("Выбран Bogo Sort");
    sorter = new BogoSorter();
    break;case 13:
    Debug.Log("Выбран std::stable_sort");
    sorter = new StableSorter();
    break;
    }

    counter.ResetCounter();
    values = (int[])originalValues.Clone();
    visualizer.CreateBars(values);
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
                visualizer.SetPseudoCode(new string[]
{
    "for i = 0 .. n-2",
    "for j = 0 .. n-i-2",
    "if A[j] > A[j+1]",
    "swap(A[j], A[j+1])"
});
            break;

        case 1:
            algorithmInfoText.text =
                "Insertion Sort\n" +
                "Лучший случай: O(n)\n" +
                "Средний случай: O(n²)\n" +
                "Худший случай: O(n²)\n\n" +
                "Устойчивая: Да";
                visualizer.SetPseudoCode(new string[]
{
    "for i = 1 .. n-1",
    "j = i",
    "while j > 0",
    "swap(A[j], A[j-1])"
});
            break;

        case 2:
            algorithmInfoText.text =
                "Selection Sort\n" +
                "Лучший случай: O(n²)\n" +
                "Средний случай: O(n²)\n" +
                "Худший случай: O(n²)\n\n" +
                "Устойчивая: Нет";
                visualizer.SetPseudoCode(new string[]
{
    "for i = 0 .. n-2",
    "найти минимум",
    "swap(A[i], A[min])",
    "следующий проход"
});
            break;
case 3:
    algorithmInfoText.text =
        "Quick Sort\n" +
        "Лучший случай: O(n log n)\n" +
        "Средний случай: O(n log n)\n" +
        "Худший случай: O(n²)\n\n" +
        "Устойчивая: Нет";

                visualizer.SetPseudoCode(new string[]
{
        "if left >= right return\n" ,
        "pivot = A[(left + right) / 2]\n" ,
        "while i <= j\n" ,
        "    обмен элементов\n" ,
        "QuickSort(left, j)\n" ,
        "QuickSort(i, right)"
        });
    break;
    case 4:
        algorithmInfoText.text =
        "Merge Sort\n" +
        "Лучший случай: O(n log n)\n" +
        "Средний случай: O(n log n)\n" +
        "Худший случай: O(n log n)\n\n" +
        "Устойчивая: Да";
    visualizer.SetPseudoCode(new string[]
{
    "if left >= right return",
    "mid = (left + right) / 2",
    "MergeSort(left, mid)",
    "MergeSort(mid + 1, right)",
    "Merge(left, mid, right)",
    "while i <= mid && j <= right",
    "сравнение элементов",
    "запись элемента"
});
    break; 
    case 5:
    algorithmInfoText.text =
        "Heap Sort\n" +
        "Лучший случай: O(n log n)\n" +
        "Средний случай: O(n log n)\n" +
        "Худший случай: O(n log n)\n\n" +
        "Устойчивая: Нет";

    visualizer.SetPseudoCode(new string[]
    {
"Построить максимальную кучу",
"Проверить вершину и потомков",
"Если потомок больше вершины — обмен",
"Восстановить кучу рекурсивно",
"Переместить максимум в конец",
"Уменьшить размер кучи"
    });
        break; 
case 6:
    algorithmInfoText.text =
        "Shell Sort\n" +
        "Лучший случай: O(n log n)\n" +
        "Средний случай: ≈ O(n^1.5)\n" +
        "Худший случай: O(n²)\n\n" +
        "Устойчивая: Нет";

    visualizer.SetPseudoCode(new string[]
    {
        "gap = n / 2",
        "Пока gap > 0",
        "Взять следующий элемент",
        "Сравнить элементы через gap",
        "Сдвинуть элементы вправо",
        "Вставить элемент на место",
        "gap = gap / 2"
    });

    break;case 7:
    algorithmInfoText.text =
        "Counting Sort\n" +
        "Лучший случай: O(n + k)\n" +
        "Средний случай: O(n + k)\n" +
        "Худший случай: O(n + k)\n\n" +
        "Устойчивая: Да";

    visualizer.SetPseudoCode(new string[]
    {
        "Найти максимальный элемент",
        "Создать массив count[]",
        "Подсчитать количество каждого значения",
        "Вычислить префиксные суммы",
        "Записать элементы в выходной массив",
        "Скопировать результат обратно"
    });

    break;case 8:
    algorithmInfoText.text =
        "Radix Sort\n" +
        "Лучший случай: O(d(n + k))\n" +
        "Средний случай: O(d(n + k))\n" +
        "Худший случай: O(d(n + k))\n\n" +
        "Устойчивая: Да";

    visualizer.SetPseudoCode(new string[]
    {
        "Найти максимальный элемент",
        "Для каждого разряда",
        "Выполнить Counting Sort",
        "Перейти к следующему разряду",
        "Повторить до максимального",
        "Массив отсортирован"
    });

    break;case 9:
    algorithmInfoText.text =
        "Radix Sort (MSD)\n" +
        "Лучший случай: O(d(n + k))\n" +
        "Средний случай: O(d(n + k))\n" +
        "Худший случай: O(d(n + k))\n\n" +
        "Устойчивая: Да";

    visualizer.SetPseudoCode(new string[]
    {
        "Найти старший разряд",
        "Разделить элементы по текущему разряду",
        "Распределить элементы по корзинам",
        "Объединить корзины обратно",
        "Рекурсивно сортировать каждую корзину",
        "Массив отсортирован"
    });

    break;case 10:
    algorithmInfoText.text =
        "Cocktail Shaker Sort\n" +
        "Лучший случай: O(n)\n" +
        "Средний случай: O(n²)\n" +
        "Худший случай: O(n²)\n\n" +
        "Устойчивая: Да";

    visualizer.SetPseudoCode(new string[]
    {
        "Идти слева направо",
        "Сравнить соседние элементы",
        "Обменять при необходимости",
        "Идти справа налево",
        "Повторять пока есть обмены",
        "Сортировка завершена"
    });

    break;case 11:
    algorithmInfoText.text =
        "Gnome Sort\n" +
        "Лучший случай: O(n)\n" +
        "Средний случай: O(n²)\n" +
        "Худший случай: O(n²)\n\n" +
        "Устойчивая: Да";

    visualizer.SetPseudoCode(new string[]
    {
        "Начать со второго элемента",
        "Сравнить соседние элементы",
        "Если порядок верный — идти вперёд",
        "Иначе выполнить обмен",
        "Сдвинуться назад",
        "Сортировка завершена"
    });

    break;case 12:
    algorithmInfoText.text =
        "Bogo Sort\n" +
        "Лучший случай: O(n)\n" +
        "Средний случай: O((n+1)!)\n" +
        "Худший случай: Бесконечность\n\n" +
        "Устойчивая: Нет";

    visualizer.SetPseudoCode(new string[]
    {
        "Проверить, отсортирован ли массив",
        "Если нет — случайно перемешать",
        "Повторять проверку",
        "Повторять до сортировки",
        "Или остановиться по таймеру",
        "Сортировка завершена"
    });

    break;case 13:
    algorithmInfoText.text =
        "std::stable_sort (Adaptive Merge Sort)\n" +
        "Лучший случай: O(n log n)\n" +
        "Средний случай: O(n log n)\n" +
        "Худший случай: O(n log n)\n\n" +
        "Устойчивая: Да";

    visualizer.SetPseudoCode(new string[]
    {
        "Разделить массив пополам",
        "Рекурсивно отсортировать левую часть",
        "Рекурсивно отсортировать правую часть",
        "Слить две отсортированные части",
        "Сохранять порядок одинаковых элементов",
        "Сортировка завершена"
    });

    break;  }
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
