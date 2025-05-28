using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GestorTiempo : MonoBehaviour
{
    [Header("Referencia al texto del temporizador")]
    public TextMeshProUGUI textoTemporizador;

    [Header("Cuenta atrás (valor por defecto, se sobrescribe según dificultad)")]
    [Tooltip("Tiempo inicial de la cuenta atrás en segundos")]
    public float tiempoInicial = 240f;
    private float tiempoRestante;

    [Header("Sonido de tic-tac (opcional)")]
    public AudioClip tickClip;
    [Header("Parámetros de tic progresivo")]
    public float maxTicksPorSegundo = 5f;

    private AudioSource audioSource;
    private bool timerActivo = false;

    private void Start()
    {
        // → Leemos la dificultad guardada (0=fácil,1=normal,2=difícil)
        int diff = PlayerPrefs.GetInt("difficulty", 1);

        switch (diff)
        {
            case 0: tiempoInicial = 240f; break; // Fácil
            case 1: tiempoInicial = 180f; break; // Normal
            case 2: tiempoInicial = 120f; break; // Difícil
        }

        // Inicializamos el temporizador
        tiempoRestante = tiempoInicial;
        timerActivo = true;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        ActualizarUI();
        if (tickClip != null)
            StartCoroutine(TickProgressivo());
    }

    private void Update()
    {
        if (!timerActivo) return;

        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            timerActivo = false;
            IniciarGameOver();
        }
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        int m = Mathf.FloorToInt(tiempoRestante / 60f);
        int s = Mathf.FloorToInt(tiempoRestante % 60f);
        textoTemporizador.text = $"Tiempo: {m:00}:{s:00}";
    }

    private IEnumerator TickProgressivo()
    {
        while (timerActivo)
        {
            audioSource.PlayOneShot(tickClip);
            float ratio = 1f - (tiempoRestante / tiempoInicial);
            float tps = Mathf.Lerp(1f, maxTicksPorSegundo, ratio);
            yield return new WaitForSeconds(1f / tps);
        }
    }

    private void IniciarGameOver()
    {
        // Guardar estadísticas
        PlayerPrefs.SetFloat("LastTimeRemaining", tiempoRestante);
        var gTar = FindObjectOfType<GestorTarjetas>();
        int tarjetas = gTar != null ? gTar.UltimasTarjetasRecogidas : 0;
        PlayerPrefs.SetInt("LastTarjetas", tarjetas);
        PlayerPrefs.Save();

        SceneManager.LoadScene("FinalDerrota");
    }

    public void DetenerTimer() => timerActivo = false;
    public float ObtenerTiempoRestante() => tiempoRestante;
}
