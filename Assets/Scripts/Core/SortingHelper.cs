using System.Collections;

public static class SortingHelper
{
    public static IEnumerator WaitForNextStep()
    {
        while (SortingState.IsPaused)
        {
            if (SortingState.StepRequested)
            {
                SortingState.StepRequested = false;
                yield break;
            }

            yield return null;
        }
    }
}