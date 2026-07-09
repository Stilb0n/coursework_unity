using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimationSettings : MonoBehaviour
{
    public Slider speedSlider;
    public TextMeshProUGUI sortSpeedText;   
    public static float Delay = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    AnimationSettings.Delay = speedSlider.value; 
    ChangeSpeed();   
    }

    public void ChangeSpeed()
{
    AnimationSettings.Delay = speedSlider.value;
    sortSpeedText.text = $"Скорость сортировки: {speedSlider.value:0.00}";
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
