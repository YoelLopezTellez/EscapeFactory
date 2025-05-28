using UnityEngine;
using System.Collections;

public class MenuFadeIn : MonoBehaviour
{
    [Tooltip("CanvasGroup a fundir.")]
    public CanvasGroup canvasGroup;
    [Tooltip("Duración del fade-in en segundos.")]
    public float duration = 1f;

    private void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}
