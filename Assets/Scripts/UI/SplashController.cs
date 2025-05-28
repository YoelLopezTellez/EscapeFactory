using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashController : MonoBehaviour
{
    [Header("Tiempo en segundos antes de iniciar el fade")]
    public float delay = 3f;

    [Header("Duración del fade (segundos)")]
    public float fadeDuration = 1f;

    [Header("El CanvasGroup del Splash (para controlar la opacidad)")]
    public CanvasGroup canvasGroup;

    [Header("Nombre exacto de la escena del menú principal")]
    public string escenaMenu = "MenuPrincipal";

    private IEnumerator Start()
    {
        // 1) Asegurarnos de que el CanvasGroup empieza totalmente opaco
        if (canvasGroup != null)
            canvasGroup.alpha = 1f;

        // 2) Esperar el tiempo de visualización de la splash
        yield return new WaitForSeconds(delay);

        // 3) Si tenemos el CanvasGroup, hacer fade out
        if (canvasGroup != null)
            yield return StartCoroutine(Fade(1f, 0f));

        // 4) Cargar el menú principal
        SceneManager.LoadScene(escenaMenu);
    }

    // Coroutine que interpola la alpha del CanvasGroup
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}
