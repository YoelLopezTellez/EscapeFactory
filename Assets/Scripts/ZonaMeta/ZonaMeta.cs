using UnityEngine;
using UnityEngine.SceneManagement;

public class ZonaMeta : MonoBehaviour
{
    [Header("Gestión de UI")]
    [Tooltip("Arrastra aquí tu gestor de mensajes (MostrarMensaje).")]
    public MostrarMensaje gestorMensaje;

    [Header("Carga de nivel")]
    [Tooltip("Nombre exacto de la siguiente escena en Build Settings.")]
    public string nombreSiguienteEscena;

    private bool nivelTerminado = false;

    private void OnTriggerEnter(Collider other)
    {
        // Solo reaccionar una vez y solo si colisiona el jugador
        if (!nivelTerminado && other.CompareTag("Player"))
        {
            nivelTerminado = true;
            Debug.Log("¡Meta alcanzada!");

            // Mostrar mensaje de victoria
            if (gestorMensaje != null)
                gestorMensaje.Mostrar("¡Has llegado a la meta!");

            // Esperar 2 segundos y cargar la siguiente escena
            Invoke(nameof(CargarSiguienteEscena), 2f);
        }
    }

    private void CargarSiguienteEscena()
    {
        if (!string.IsNullOrEmpty(nombreSiguienteEscena))
            SceneManager.LoadScene(nombreSiguienteEscena);
        else
            Debug.LogWarning("ZonaMeta: no has puesto el nombre de la siguiente escena.");
    }
}
