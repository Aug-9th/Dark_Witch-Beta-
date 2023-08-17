using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    public void SetValue(int value)
    {
        slider.value = value;
    }
}
