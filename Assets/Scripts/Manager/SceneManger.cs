using UnityEngine;
using System.Collections;

public class SceneManger : MonoBehaviour 
{
    public GameObject Particles;
    public GameObject SignUpPanel;
    public GameObject SignInPanel;

    
    protected AudioSource audio;

    private bool isSignUp = false;
    private float time = 0.5f;

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

    public void PlaneMove()
    {
       
        if (isSignUp)
        {
            iTween.MoveTo(SignUpPanel, SignUpPanel.transform.position + new Vector3(5330, 0, 0), time);
            iTween.MoveTo(SignInPanel, SignInPanel.transform.position + new Vector3(5330, 0, 0), time);

            isSignUp = !isSignUp;
        }
        else
        {
            iTween.MoveTo(SignUpPanel, SignUpPanel.transform.position - new Vector3(5330, 0, 0), time);
            iTween.MoveTo(SignInPanel, SignInPanel.transform.position - new Vector3(5330, 0, 0), time);

            isSignUp = !isSignUp;
        }
    }
}
