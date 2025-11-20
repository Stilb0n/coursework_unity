using System.Collections;   // обязательно для IEnumerator

public interface ISorter
{
    IEnumerator Sort(int[] array, IVisualizerAPI visualizer);
}
