using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FaderScript : MonoBehaviour
{
    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.2f;

    private int drawDepth = -2;
    public float alpha = -1f;
    private int fadeDir = -1;

    void Start()
    {
    }

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        //Debug.Log ("Before alpha : " + alpha);
        alpha = Mathf.Clamp01(alpha);

        //Debug.Log ("Alpha : " + alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return 1 / fadeSpeed;
    }



    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        BeginFade(-1);
    }
}
