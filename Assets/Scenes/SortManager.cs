using System.Collections;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    public int arraySize = 10;
    public float barWidth = 0.8f;
    public float stepDelay = 0.2f;

    private GameObject[] bars;
    private float[] heights;

    void Start()
    {
        GenerateArray();
        StartCoroutine(BubbleSort());
    }

    void GenerateArray()
    {
        bars = new GameObject[arraySize];
        heights = new float[arraySize];

        for (int i = 0; i < arraySize; i++)
        {
            float height = Random.Range(1f, 10f);
            heights[i] = height;

            GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bar.transform.localScale = new Vector3(barWidth, height, 1);
            bar.transform.position = new Vector3(i * (barWidth + 0.1f), height / 2f, 0);
            bar.GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.cyan, height / 10f);

            bars[i] = bar;
        }
    }

    IEnumerator BubbleSort()
    {
        Debug.Log("Sorting started!");

        for (int i = 0; i < arraySize - 1; i++)
        {
            for (int j = 0; j < arraySize - i - 1; j++)
            {
                if (heights[j] > heights[j + 1])
                {
                    // 1. Меняем высоты в логическом массиве
                    float tempHeight = heights[j];
                    heights[j] = heights[j + 1];
                    heights[j + 1] = tempHeight;

                    // 2. Меняем ссылки на кубы
                    GameObject tempBar = bars[j];
                    bars[j] = bars[j + 1];
                    bars[j + 1] = tempBar;

                    // 3. Меняем позиции X (Y = половина высоты)
                    Vector3 posJ = bars[j].transform.position;
                    Vector3 posJ1 = bars[j + 1].transform.position;

                    bars[j].transform.position = new Vector3(posJ1.x, heights[j] / 2f, 0);
                    bars[j + 1].transform.position = new Vector3(posJ.x, heights[j + 1] / 2f, 0);

                    yield return new WaitForSeconds(stepDelay);
                }
            }
        }

        Debug.Log("Sorting finished!");
    }
}
