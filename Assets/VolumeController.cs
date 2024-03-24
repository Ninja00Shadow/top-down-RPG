using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public float volume = 0.5f;
    public float minVolume = 0.0f;
    public float maxVolume = 1.0f;

    private AudioSource audio;
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        audio.volume = volume;
        print(audio.clip.name + " volume: " + audio.volume);
    }
    
    void OnGUI()
    {
        GUI.Label(new Rect(25, 5, 100, 30), "Volume");
        volume = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), volume, minVolume, maxVolume);
    }
}
