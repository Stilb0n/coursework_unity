using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimationSettings : MonoBehaviour
{
    public Slider speedSlider;
    public TextMeshProUGUI sortSpeedText;

    public static float Speed = 1f;

    public static float Delay
    {
        get
        {
            if (Speed >= 50f)
                return 0f;

            return 0.2f / Speed;
        }
    }

    void Start()
    {
        Speed = speedSlider.value;
        ChangeSpeed();
    }

    public void ChangeSpeed()
    {
        Speed = speedSlider.value;
        sortSpeedText.text = $"Скорость:";
    }
}