using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInandOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;			// Speed that the screen fades to and from black.

    Image image;


    private bool sceneStarting = true;		// Whether or not the scene is still fading in.
    private bool sceneEnding = false;

    private bool sceneEnded = false;
    public bool SceneEnded
    {
        get { return sceneEnded; }
    }		


    void Awake()
    {
        // Set the texture so that it is the the size of the screen and covers it.
        image = transform.GetComponent<Image>();
    }


    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
            // ... call the StartScene function.
            StartScene();

        if (sceneEnding)
            EndScene();
    }


    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (image.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            image.color = Color.clear;
            image.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }


    public void EndScene()
    {
        // Make sure the texture is enabled.
        image.enabled = true;
        sceneStarting = false;
        sceneEnding = true;
        fadeSpeed = 5f;
        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (image.color.a >= 0.95f)
        {
            sceneEnded = true;
        }
    }
}
