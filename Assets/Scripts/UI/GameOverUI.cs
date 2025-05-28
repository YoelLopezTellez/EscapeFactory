using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI txtTiempo;
    public TextMeshProUGUI txtTarjetas;
    public TextMeshProUGUI txtPuntuacionDerrota;

    [Header("Nombres de escena")]
    public string escenaJuego = "Juego";
    public string escenaMenu  = "MenuPrincipal";

    private void OnEnable()
    {
        // Mostrar y desbloquear cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;

        // Leer estadísticas guardadas
        float timeRemain = PlayerPrefs.GetFloat("LastTimeRemaining", 0f);
        int cards        = PlayerPrefs.GetInt("LastTarjetas", 0);
        int score        = PlayerPrefs.GetInt("LastScore", 0);

        // Formatear y mostrar tiempo restante
        int m = Mathf.FloorToInt(timeRemain / 60f);
        int s = Mathf.FloorToInt(timeRemain % 60f);
        txtTiempo.text = $"Tiempo restante: {m:00}:{s:00}";

        // Mostrar tarjetas recogidas
        txtTarjetas.text = $"Tarjetas recogidas: {cards} / 3";

        // Mostrar puntuación final
        txtPuntuacionDerrota.text = $"Puntuación: {score}";
    }

    /// <summary>
    /// Vuelve a cargar la escena de juego
    /// </summary>
    public void Reintentar()
    {
        SceneManager.LoadScene(escenaJuego);
    }

    /// <summary>
    /// Vuelve al menú principal
    /// </summary>
    public void Salir()
    {
        SceneManager.LoadScene(escenaMenu);
    }

    /// <summary>
    /// Método alternativo si quieres un botón específico para cerrar la aplicación
    /// </summary>
    public void CerrarAplicacion()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
