using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public float fadeInTime;
    public float fadeOutTime;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(0f, 0f, 0f, 0f);
    }

    public void FadeIn() {
        StartCoroutine(Fade(0f, 1f, fadeInTime));
    }

    public void FadeOut() {
        StartCoroutine(Fade(1f, 0f, fadeOutTime));
    }

    IEnumerator Fade(float a1, float a2, float time) {
        Color start = Color.black;
        start.a = a1;
        Color end = Color.black;
        end.a = a2;

        float t = 0;
        while (t < time) {
            image.color = Color.Lerp(start, end, t / time);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }

        image.color = end;
    }
}
