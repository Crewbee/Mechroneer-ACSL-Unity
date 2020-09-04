using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    public Color ambientDarkest;
    public Color ambientLightest;

    public float sliderValue { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        sliderValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.ambientLight = Color.Lerp(ambientDarkest, ambientLightest, sliderValue);
    }
}
