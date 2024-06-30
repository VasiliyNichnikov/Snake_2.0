using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeMenue : MonoBehaviour
{
    public float fade_speed = 0.5f;
    public float target_alpha = 0.5f;  // Set the target alpha for partial darkening
    private Image fade_image;
    private Color color;
    private bool isDarkened = false;

    void Start()
    {
        fade_image = GetComponent<Image>();
        color = fade_image.color;
    }

    public void ToggleFade()
    {
        if (isDarkened)
        {
            StartCoroutine(Fade(false));
        }
        else
        {
            StartCoroutine(Fade(true));
        }
        isDarkened = !isDarkened;
    }

    public IEnumerator Fade(bool darken)
    {
        if (darken)
        {
            yield return StartCoroutine(Darken());
        }
        else
        {
            yield return StartCoroutine(Lighten());
        }
    }

    private IEnumerator Darken()
    {
        while (color.a < target_alpha)
        {
            color.a += fade_speed * Time.deltaTime;
            fade_image.color = color;
            yield return null;
        }
        color.a = target_alpha;
        fade_image.color = color;
    }

    private IEnumerator Lighten()
    {
        while (color.a > 0f)
        {
            color.a -= fade_speed * Time.deltaTime;
            fade_image.color = color;
            yield return null;
        }
        color.a = 0f;
        fade_image.color = color;
    }
}