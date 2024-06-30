using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour
{
    public Text buttonText;
    public float hoverScaleFactor = 1.2f;
    public Color hoverColor = Color.red;
    private Color normalColor;
    private Vector3 originalScale;

    void Start()
    {
        normalColor = buttonText.color;
        originalScale = buttonText.transform.localScale;
    }

    public void OnPointerEnter()
    {
        buttonText.transform.localScale = originalScale * hoverScaleFactor;
        buttonText.color = hoverColor;
    }

    public void OnPointerExit()
    {
        buttonText.transform.localScale = originalScale;
        buttonText.color = normalColor;
    }
}
