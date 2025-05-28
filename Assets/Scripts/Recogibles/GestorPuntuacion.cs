using UnityEngine;
using TMPro;

public class GestorPuntuacion : MonoBehaviour
{
    [Header("Referencia al texto de puntuación")]
    public TextMeshProUGUI textoPuntuacion;

    [Header("Puntos por tarjeta")]
    public int puntosPorTarjeta = 100;

    private int puntuacionActual = 0;

    private void Start()
    {
        if (textoPuntuacion == null)
            Debug.LogWarning("GestorPuntuacion: no has asignado 'textoPuntuacion' en el Inspector.");
        ActualizarUI();
    }

    /// <summary>
    /// Llamar cada vez que se recoge una tarjeta.
    /// </summary>
    public void AñadirPuntosPorTarjeta()
    {
        puntuacionActual += puntosPorTarjeta;
        ActualizarUI();
    }

    /// <summary>
    /// Al terminar el nivel, aplicar bonus por tiempo restante.
    /// </summary>
    public void AplicarBonusTiempo(float segundosRestantes, int puntosPorSegundo = 1)
    {
        int bonus = Mathf.FloorToInt(segundosRestantes) * puntosPorSegundo;
        puntuacionActual += bonus;
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        textoPuntuacion.text = $"Puntos: {puntuacionActual}";
    }

    /// <summary>
    /// Devuelve la puntuación final.
    /// </summary>
    public int ObtenerPuntuacion() => puntuacionActual;

    
}


