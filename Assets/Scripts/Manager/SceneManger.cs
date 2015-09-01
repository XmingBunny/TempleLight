using UnityEngine;
using System.Collections;

public class SceneManger : MonoBehaviour 
{
    public GameObject Particles;
    protected AudioSource audio;

    void Awake()
    {
        audio = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<AudioSource>();
    }

    public void SetMusic(bool isOn)
    {
        audio.enabled = isOn;
    }

    public void SetParticles(bool isOn)
    {
        Particles.SetActive(isOn);
    }
}
