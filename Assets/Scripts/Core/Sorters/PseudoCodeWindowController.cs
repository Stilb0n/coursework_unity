using UnityEngine;

public class PseudoCodeWindowController : MonoBehaviour
{
    [SerializeField] private GameObject pseudoCodeWindow;

    public void Open()
    {
        pseudoCodeWindow.SetActive(true);
    }

    public void Close()
    {
        pseudoCodeWindow.SetActive(false);
    }
}