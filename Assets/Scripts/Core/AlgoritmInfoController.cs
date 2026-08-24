using UnityEngine;

public class AlgoritmInfoController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject algoritmInfoWindow;

    public void Open()
    {
        algoritmInfoWindow.SetActive(true);
    }

    public void Close()
    {
        algoritmInfoWindow.SetActive(false);
    }
}
