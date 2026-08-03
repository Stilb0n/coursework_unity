using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class BenchmarkUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI resultsText;

    public void ShowResults(List<SortBenchmarkResult> results)
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine(
            $"{"Алгоритм",-22} {"Время",-12} {"Сравнения",-12} {"Обмены"}"
        );

        builder.AppendLine(new string('-', 70));

        foreach (var result in results)
        {
            if (result.TimeMs < 0)
            {
                builder.AppendLine(
                    $"{result.AlgorithmName,-22}" +
                    $"{"Недоступно",-12}" +
                    $"{"-",-12}" +
                    $"-"
                );

                continue;
            }

            builder.AppendLine(
                $"{result.AlgorithmName,-22}" +
                $"{result.TimeMs,8:F4} ms   " +
                $"{result.Comparisons,10}" +
                $"{result.Swaps,10}"
            );
        }

        resultsText.text = builder.ToString();

        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}