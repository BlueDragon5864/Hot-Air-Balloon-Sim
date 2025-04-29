using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(float health) {
        slider.value = health;
    }
}
