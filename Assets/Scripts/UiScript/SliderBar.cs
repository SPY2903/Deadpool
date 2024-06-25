using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] private string sliderName;
    [SerializeField] private Slider slider;
    [SerializeField] private int maxValue;
    [SerializeField] private AudioValue audioValue;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxValue;
        if (sliderName.Equals("music"))
        {
            slider.value = audioValue.musicVolume;
        }
        else if (sliderName.Equals("sound"))
        {
            slider.value = audioValue.soundVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.onValueChanged.AddListener(delegate { ValueChange();});
    }
    public void SetValue(int value)
    {
        slider.value = value;
    }
    public void ValueChange()
    {
        if (sliderName.Equals("music"))
        {
            AudioManager.Instance.asBg.volume = slider.value;
            audioValue.musicVolume = slider.value;
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.asUi.Play();
            }
        }
        else if (sliderName.Equals("sound"))
        {
            AudioManager.Instance.asUi.volume = slider.value;
            audioValue.soundVolume = slider.value;
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.asUi.Play();
            }
        }
    }
}
