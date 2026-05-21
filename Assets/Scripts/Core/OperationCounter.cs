using TMPro;
using UnityEngine;

public class OperationCounter : MonoBehaviour, IOperationCounter
{
    public TextMeshProUGUI comparisonsText;
    public TextMeshProUGUI swapsText;

    private int comparisons;
    private int swaps;

    public void ResetCounter()
    {
        comparisons = 0;
        swaps = 0;
        UpdateUI();
    }

    public void IncrementComparisons()
    {
        comparisons++;
        UpdateUI();
    }

    public void IncrementSwaps()
    {
        swaps++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        comparisonsText.text = "Comparisons: " + comparisons;
        swapsText.text = "Swaps: " + swaps;
    }
}