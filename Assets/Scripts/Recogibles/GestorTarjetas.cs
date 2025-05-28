using UnityEngine;
using TMPro;
using DesignPatterns.DIP;  // para Door

public class GestorTarjetas : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI textoTarjetas;

    [Header("UI Manager")]
    public MostrarMensaje gestorMensaje;

    [Header("Configuración")]
    [Tooltip("Número de tarjetas que el jugador debe recoger.")]
    public int totalTarjetasNecesarias = 3;

    [Header("Puerta de salida")]
    [Tooltip("Arrastra aquí tu GameObject 'Door' con el script Door.")]
    public Door puerta;

    // Exponemos para lectura externa, pero NO es singleton
    public int UltimasTarjetasRecogidas { get; private set; } = 0;

    private int tarjetasRecogidas = 0;

    private void Start()
    {
        // Asegurar que arrancamos a cero
        tarjetasRecogidas = 0;
        UltimasTarjetasRecogidas = 0;
        ActualizarTexto();

        if (gestorMensaje == null)
            gestorMensaje = FindObjectOfType<MostrarMensaje>();
    }

    /// <summary>Se llama al colisionar con una tarjeta.</summary>
    public void SumarTarjeta()
    {
        tarjetasRecogidas++;
        UltimasTarjetasRecogidas = tarjetasRecogidas;
        ActualizarTexto();

        if (tarjetasRecogidas >= totalTarjetasNecesarias)
            ActivarPuertaSalida();
    }

    private void ActualizarTexto()
    {
        if (textoTarjetas != null)
            textoTarjetas.text = $"Tarjetas: {tarjetasRecogidas} / {totalTarjetasNecesarias}";
    }

    private void ActivarPuertaSalida()
    {
        if (gestorMensaje != null)
            gestorMensaje.Mostrar("¡Puerta desbloqueada!");
        if (puerta != null)
            puerta.Activate();
        else
            Debug.LogWarning("GestorTarjetas: falta referencia a la puerta.");
    }
}
