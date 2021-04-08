using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public AudioMixer mixer;

    public void setlevel (float sliderValue)
    {
        mixer.SetFloat("MyExposedParam", Mathf.Log10(sliderValue)*20);
    }
}
