using System.Collections.Generic;

public class BenchmarkManager
{
    public List<SortBenchmarkResult> RunAll(int[] originalArray)
    {
        List<SortBenchmarkResult> results = new List<SortBenchmarkResult>();

        results.Add(BubbleBenchmark.Run(originalArray));
        results.Add(InsertionBenchmark.Run(originalArray));
        results.Add(SelectionBenchmark.Run(originalArray));
        results.Add(QuickBenchmark.Run(originalArray));
        results.Add(MergeBenchmark.Run(originalArray));
        results.Add(HeapBenchmark.Run(originalArray));
        results.Add(ShellSortBenchmark.Run(originalArray));
        results.Add(CountingSortBenchmark.Run(originalArray));
        results.Add(RadixSortBenchmark.Run(originalArray));
        results.Add(RadixMSDSortBenchmark.Run(originalArray));
        results.Add(CocktailShakerSortBenchmark.Run(originalArray));
        results.Add(GnomeSortBenchmark.Run(originalArray));
        results.Add(StableSortBenchmark.Run(originalArray));
        results.Add(IntroSortBenchmark.Run(originalArray));

        // Bitonic Sort
        if (IsPowerOfTwo(originalArray.Length))
        {
            results.Add(BitonicSortBenchmark.Run(originalArray));
        }
        else
        {
            results.Add(new SortBenchmarkResult
            {
                AlgorithmName = "Bitonic Sort (2ⁿ)",
                Comparisons = -1,
                Swaps = -1,
                TimeMs = -1
            });
        }

        // Bogo Sort
        if (originalArray.Length <= 8)
        {
            results.Add(BogoSortBenchmark.Run(originalArray));
        }
        else
        {
            results.Add(new SortBenchmarkResult
            {
                AlgorithmName = "Bogo Sort (≤8)",
                Comparisons = -1,
                Swaps = -1,
                TimeMs = -1
            });
        }

        return results;
    }

    private bool IsPowerOfTwo(int n)
    {
        return n > 0 && (n & (n - 1)) == 0;
    }
}