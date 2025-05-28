using UnityEngine;
using UnityEngine.SceneManagement;
using DesignPatterns.LSP;

public class GameOverOnDeath : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private string gameOverSceneName = "FinalDerrota";

    private void OnEnable()
    {
        playerHealth.Died.AddListener(OnPlayerDied);
    }

    private void OnDisable()
    {
        playerHealth.Died.RemoveListener(OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        SaveStats();
        SceneManager.LoadScene(gameOverSceneName);
    }

    private void SaveStats()
    {
        // Tiempo restante
        var gt = FindObjectOfType<GestorTiempo>();
        float tiempo = gt != null ? gt.ObtenerTiempoRestante() : 0f;
        PlayerPrefs.SetFloat("LastTimeRemaining", tiempo);

        // Tarjetas recogidas
        var gTar = FindObjectOfType<GestorTarjetas>();
        int tarjetas = gTar != null ? gTar.UltimasTarjetasRecogidas : 0;
        PlayerPrefs.SetInt("LastTarjetas", tarjetas);

        // Puntuación
        var gp = FindObjectOfType<GestorPuntuacion>();
        int score = gp != null ? gp.ObtenerPuntuacion() : 0;
        PlayerPrefs.SetInt("LastScore", score);

        PlayerPrefs.Save();
    }
}
