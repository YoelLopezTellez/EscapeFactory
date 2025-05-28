using UnityEngine;
using UnityEngine.SceneManagement;

public class FinNivel : MonoBehaviour
{
    [Header("Gestión de escenas")]
    [Tooltip("Nombre exacto de la escena de victoria")]
    public string escenaVictoria = "FinalVictoria";

    [Header("Opciones de puntuación")]
    [Tooltip("Puntos por segundo restante")]
    public int puntosPorSegundo = 2;

    private bool nivelTerminado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (nivelTerminado || !other.CompareTag("Player"))
            return;

        nivelTerminado = true;

        // 1) Parar el temporizador
        GestorTiempo gt = FindObjectOfType<GestorTiempo>();
        if (gt != null)
            gt.DetenerTimer();

        // 2) Calcular tiempo restante
        float segundosRest = gt != null ? gt.ObtenerTiempoRestante() : 0f;

        // 3) Aplicar bonus de tiempo a la puntuación
        GestorPuntuacion gp = FindObjectOfType<GestorPuntuacion>();
        if (gp != null)
            gp.AplicarBonusTiempo(segundosRest, puntosPorSegundo);

        // 4) Guardar estadísticas en PlayerPrefs
        PlayerPrefs.SetFloat("LastTimeRemaining", segundosRest);
        var gTar = FindObjectOfType<GestorTarjetas>();
int tarjetas = gTar != null ? gTar.UltimasTarjetasRecogidas : 0;
PlayerPrefs.SetInt("LastTarjetas", tarjetas);

        PlayerPrefs.SetInt("LastScore", gp != null ? gp.ObtenerPuntuacion() : 0);
        PlayerPrefs.Save();

        // 5) Cargar la escena de victoria
        SceneManager.LoadScene(escenaVictoria);
    }
}
