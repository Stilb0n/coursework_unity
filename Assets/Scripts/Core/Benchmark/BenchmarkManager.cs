using System.Collections.Generic;

public class BenchmarkManager
{
    public List<SortBenchmarkResult> RunAll(int[] originalArray)
    {
        List<SortBenchmarkResult> results = new List<SortBenchmarkResult>();

        // Здесь позже будем запускать все алгоритмы
            results.Add(BubbleBenchmark.Run(originalArray));

        return results;
    }
}