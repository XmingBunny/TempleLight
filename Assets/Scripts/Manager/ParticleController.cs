using UnityEngine;
using System.Collections;


public class ParticleController : MonoBehaviour
{
    public float ExistTime = 1.0f;
    protected float timer = 0;

    void Start()
    {

    }

    public void StartTimer()
    {
        StartCoroutine(EndParticle());
    }

    IEnumerator EndParticle()
    {
        yield return new WaitForSeconds(ExistTime);

        Destroy(gameObject);
    }
}


