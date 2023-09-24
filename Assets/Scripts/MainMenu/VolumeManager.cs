using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static float currentVolume = 1f;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = currentVolume;
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        AudioListener.volume = volume;
    }
}
