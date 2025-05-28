using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VictoriaUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI txtTiempoRestante;
    public TextMeshProUGUI txtTarjetasRecogidas;
    public TextMeshProUGUI txtPuntuacionFinal;

    [Header("Escenas")]
    public string escenaJuego       = "Juego";
    public string escenaMenu       = "MenuPrincipal";

    private void OnEnable()
    {
        // 0) Mostrar y desbloquear el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;

        // Leer estadísticas guardadas
        float timeRemain = PlayerPrefs.GetFloat("LastTimeRemaining", 0f);
        int cards       = PlayerPrefs.GetInt("LastTarjetas", 0);
        int score       = PlayerPrefs.GetInt("LastScore",     0);

        // Formatear tiempo
        int m = Mathf.FloorToInt(timeRemain / 60f);
        int s = Mathf.FloorToInt(timeRemain % 60f);
        txtTiempoRestante.text = $"Tiempo restante: {m:00}:{s:00}";

        // Mostrar tarjetas
        txtTarjetasRecogidas.text = $"Tarjetas recogidas: {cards} / 3";

        // Mostrar puntuación
        txtPuntuacionFinal.text = $"Puntuación: {score}";
    }

    // Métodos para los botones del panel de victoria
    public void JugarDeNuevo()
    {
        SceneManager.LoadScene(escenaJuego);
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene(escenaMenu);
    }
}
