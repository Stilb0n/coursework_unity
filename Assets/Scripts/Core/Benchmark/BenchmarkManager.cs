using System.Collections.Generic;

public class BenchmarkManager
{
    public List<SortBenchmarkResult> RunAll(int[] originalArray)
    {
        List<SortBenchmarkResult> results = new List<SortBenchmarkResult>();

        // Здесь позже будем запускать все алгоритмы
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
        return results;
    }
}