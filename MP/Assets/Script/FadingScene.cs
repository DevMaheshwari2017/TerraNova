using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadingScene : MonoBehaviour
{
    [SerializeField]
    private Image img;
    [SerializeField]
    private AnimationCurve anim_Curve;


    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(int scene)
    {
        StartCoroutine(FadeOut(scene));
    }
    IEnumerator FadeIn()
    {
        float time = 1f;
        while(time > -0.5f)
        {
            time -= Time.deltaTime;
            // a = alpha
            float a = anim_Curve.Evaluate(time);
            img.color = new Color(0f, 0f, 0f, a); 
            //skip to next frame
            yield return 0;
        }
    } 
    IEnumerator FadeOut(int Scene)
    {
        float time = 0f;
        while(time < 1f)
        {
            time += Time.deltaTime;
            // a = alpha
            float a = anim_Curve.Evaluate(time);
            img.color = new Color(0f, 0f, 0f, a); 
            //skip to next frame
            yield return 0;
        }

        SceneManager.LoadScene(Scene);
    }

}
