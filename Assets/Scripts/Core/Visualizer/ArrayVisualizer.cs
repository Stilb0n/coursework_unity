using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ArrayVisualizer : MonoBehaviour, IVisualizerAPI
{
    public GameObject barPrefab;
    public TextMeshProUGUI statusText;
    public float barWidth = 0.8f;
    public float spacing = 0.1f;
    private bool[] sorted;
    public TextMeshProUGUI[] pseudoCodeLines;
    private List<GameObject> bars = new List<GameObject>();
    private List<Color> originalColors = new List<Color>();
public void HighlightPseudoCodeLine(int line)
{
    Debug.Log("Подсветка псевдокода: " + line);

    for (int i = 0; i < pseudoCodeLines.Length; i++)
    {
        pseudoCodeLines[i].color = Color.white;
    }

    if (line >= 0 && line < pseudoCodeLines.Length)
        pseudoCodeLines[line].color = Color.yellow;
}
public void SetPseudoCode(string[] lines)
{
    for (int i = 0; i < pseudoCodeLines.Length; i++)
    {
        if (i < lines.Length)
            pseudoCodeLines[i].text = lines[i];
        else
            pseudoCodeLines[i].text = "";
    }
}
    // Создание столбиков
    public void CreateBars(int[] values)
    {
        ClearBars();
        sorted = new bool[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            GameObject bar = Instantiate(barPrefab, transform);           
            // Создаем уникальный материал для этого кубика
            Renderer renderer = bar.GetComponent<Renderer>();
             renderer.material = new Material(renderer.sharedMaterial);
           //  bars.Add(bar);
            originalColors.Add(renderer.material.color);
            float height = values[i];

            // Размер
            bar.transform.localScale = new Vector3(barWidth, height, barWidth);

            // Позиция — важно!
            bar.transform.localPosition = new Vector3(
                i * (barWidth + spacing),
                height / 2f,   // ставим нижнюю грань на y = 0
                0
            );

            bars.Add(bar);
        }
    }
public void ShowStatus(string text)
{
    statusText.text = text;
}
    public void MarkSorted(int index)
    {
        sorted[index] = true;
        bars[index].GetComponent<Renderer>().material.color = Color.green;
    }

    // Корректный плавный обмен кубиков
    public IEnumerator SwapBars(int i, int j)
    {
        GameObject barA = bars[i];
        GameObject barB = bars[j];

        Vector3 posA = barA.transform.localPosition;
        Vector3 posB = barB.transform.localPosition;

        float duration = 0.25f;
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;

            // Двигаем только X!
            barA.transform.localPosition = new Vector3(
                Mathf.Lerp(posA.x, posB.x, t),
                posA.y,        // Y сохраняем
                posA.z
            );

            barB.transform.localPosition = new Vector3(
                Mathf.Lerp(posB.x, posA.x, t),
                posB.y,
                posB.z
            );

            time += Time.deltaTime;
            yield return null;
        }

        // Фиксируем конечную позицию
        barA.transform.localPosition = new Vector3(posB.x, posA.y, posA.z);
        barB.transform.localPosition = new Vector3(posA.x, posB.y, posB.z);

        // Поменяли в списке
        bars[i] = barB;
        bars[j] = barA;
    }

    // Подсветка двух элементов с автоматическим сбросом остальных

    public void Highlight(int i, int j)
    {
        for (int k = 0; k < bars.Count; k++)
        {
            if (sorted[k])
                bars[k].GetComponent<Renderer>().material.color = Color.green;
            else
                bars[k].GetComponent<Renderer>().material.color = originalColors[k];
        }

        if (!sorted[i])
            bars[i].GetComponent<Renderer>().material.color = Color.blue;

        if (!sorted[j])
            bars[j].GetComponent<Renderer>().material.color = Color.blue;
    }

public void HighlightCompare(int i, int j)
{
    if (!sorted[i])
        bars[i].GetComponent<Renderer>().material.color = Color.red;

    if (!sorted[j])
        bars[j].GetComponent<Renderer>().material.color = Color.red;
}

    public void HighlightActive(int index)
{
    if (!sorted[index])
        bars[index].GetComponent<Renderer>().material.color = Color.yellow;
}

public void ClearHighlights()
{
    for (int i = 0; i < bars.Count; i++)
    {
        if (!sorted[i])
            bars[i].GetComponent<Renderer>().material.color = originalColors[i];
    }
}

    void ClearBars()
    {
        foreach (GameObject bar in bars)
            Destroy(bar);

        bars.Clear();
            originalColors.Clear();
    }
}
