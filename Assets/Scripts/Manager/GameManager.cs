using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public Transform ScreenFade;

    FadeInandOut fadeInandOut;

    void Awake()
    {
        fadeInandOut = GameObject.FindGameObjectWithTag(Tags.FadeInandOut).GetComponent<FadeInandOut>();
    }

    public void LoadScene(int i)
    {
        fadeInandOut.EndScene();
        StartCoroutine(DelayLoad(i));
    }

    public void LoadScene(string name)
    {

    }

    public void LoadNextScene()
    {

    }

    IEnumerator DelayLoad(int i)
    {
        if (!fadeInandOut.SceneEnded)
        {
            yield return 0;
        }

        yield return new WaitForSeconds(1f);

        Application.LoadLevel(i);
    }
}
