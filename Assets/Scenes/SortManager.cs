using UnityEngine;

public class SortManager : MonoBehaviour
{
    public int arraySize = 10;
    public float barWidth = 0.8f;
    public GameObject barPrefab;

    void Start()
    {
        GenerateArray();
    }

    void GenerateArray()
    {
        for (int i = 0; i < arraySize; i++)
        {
            // Случайная высота от 1 до 10
            float height = Random.Range(1f, 10f);

            // Создаём столбик
            GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bar.transform.localScale = new Vector3(barWidth, height, 1);
            bar.transform.position = new Vector3(i * (barWidth + 0.1f), height / 2f, 0);

            // Цвет для наглядности
            bar.GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.cyan, height / 10f);
        }

        Debug.Log("Array generated!");
    }
}
