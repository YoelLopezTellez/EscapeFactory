using UnityEngine;
using TMPro;
using System.Collections;

public class MostrarMensaje : MonoBehaviour
{
    public TextMeshProUGUI textoMensaje;

    public void Mostrar(string mensaje, float duracion = 2f)
    {
        // 1) Asegurarnos de que este GameObject (MensajeEmergente) está activo
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        // 2) Asegurarnos de que el texto esté activo
        if (!textoMensaje.gameObject.activeSelf)
            textoMensaje.gameObject.SetActive(true);

        // 3) Arrancar la coroutine
        StopAllCoroutines();
        StartCoroutine(MostrarTemporal(mensaje, duracion));
    }

    private IEnumerator MostrarTemporal(string mensaje, float duracion)
    {
        textoMensaje.text = mensaje;
        textoMensaje.gameObject.SetActive(true);
        yield return new WaitForSeconds(duracion);
        textoMensaje.gameObject.SetActive(false);
    }
}
