using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalUI : MonoBehaviour
{
    [Header("Nombre de la escena de juego")]
    [Tooltip("Escena del juego (SampleScene renombrada a ‘Juego’).")]
    public string escenaJuego = "Juego";

    [Header("Panel de opciones (opcional)")]
    [Tooltip("Arrastra aquí tu panel de Opciones si lo creas luego.")]
    public GameObject panelOpciones;

    private void Start()
    {
        // Al arrancar el menú, oculta panelOpciones si existe
        if (panelOpciones != null)
            panelOpciones.SetActive(false);
    }

    // Llamado por el botón “Iniciar Partida”
    public void IniciarPartida()
    {
        SceneManager.LoadScene(escenaJuego);
    }

    // Llamado por el botón “Continuar”
    public void ContinuarPartida()
    {
        // Aquí podrías cargar una partida guardada; de momento,
        // simplemente cargamos la misma escena de juego.
        SceneManager.LoadScene(escenaJuego);
    }

    // Llamado por el botón “Opciones”
    public void MostrarOpciones()
    {
        if (panelOpciones != null)
            panelOpciones.SetActive(true);
    }

    // Llamado por el botón “Salir”
    public void SalirJuego()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Llamar a este método desde un botón “Volver” dentro de tu panel de opciones
    public void VolverAlMenu()
    {
        if (panelOpciones != null)
            panelOpciones.SetActive(false);
    }
}
