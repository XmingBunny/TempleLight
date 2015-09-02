using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    FadeInandOut fadeOut;

    void Awake()
    {
        fadeOut = GameObject.FindGameObjectWithTag(Tags.FadeOut).GetComponent<FadeInandOut>();
    }

    public void LoadScene(int i)
    {
        fadeOut.EndScene();
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
        if (!fadeOut.SceneEnded)
        {
            yield return 0;
        }

        yield return new WaitForSeconds(0.5f);

        Application.LoadLevel(i);
    }
}
