using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValuesScript : MonoBehaviour
{
    public Slider FlipperSlider;
    public Text FlipperText;

    public static float FlipperSpeed = 1700;

    void Update()
    {
        FlipperText.text = FlipperSlider.value.ToString("f2");
        FlipperSpeed = FlipperSlider.value;
    }
}
