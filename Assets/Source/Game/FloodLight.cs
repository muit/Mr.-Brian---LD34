using UnityEngine;
using System.Collections;

public class FloodLight : MonoBehaviour
{

    new private Light light;
    new private AudioSource audio;

    public bool startsOn = true;

    public float intensity = 3.5f;
    public Light secundaryLight;
    public float secundaryIntensity = 1f;

    void Awake()
    {
        light = GetComponent<Light>();
        audio = GetComponent<AudioSource>();
        if (!startsOn)
        {
            TurnOff(false);
        }
        else
        {
            TurnOn(false);
        }
    }

    public void TurnOn(bool sound = true)
    {
        light.intensity = intensity;
        if(sound) audio.Play();
        if (secundaryLight)
        {
            secundaryLight.intensity = secundaryIntensity;
        }
    }

    public void TurnOff(bool sound = true)
    {
        light.intensity = 0;
        if (sound) audio.Play();
        if (secundaryLight)
        {
            secundaryLight.intensity = 0;
        }
    }
}
