using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Panel de Opciones dentro del PausePanel")]
    public GameObject panelOpciones;

    [Tooltip("Dropdown de dificultad (opcional, puede quedar sin asignar)")]
    public TMP_Dropdown difficultyDropdown;

    [Tooltip("Slider para ajustar el volumen del juego")]
    public Slider volumeSlider;

    private void Start()
    {
        // Empieza oculto
        if (panelOpciones != null)
            panelOpciones.SetActive(false);

        // Carga y aplica dificultad si hay dropdown
        int diff = PlayerPrefs.GetInt("difficulty", 1);
        if (difficultyDropdown != null)
        {
            difficultyDropdown.value = diff;
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        }

        // Carga y aplica volumen
        float vol = PlayerPrefs.GetFloat("volume", 1f);
        AudioListener.volume = vol;
        if (volumeSlider != null)
        {
            volumeSlider.value = vol;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    public void OnDifficultyChanged(int newValue)
    {
        PlayerPrefs.SetInt("difficulty", newValue);
        PlayerPrefs.Save();
    }

    public void OnVolumeChanged(float newVolume)
    {
        AudioListener.volume = newVolume;
        PlayerPrefs.SetFloat("volume", newVolume);
        PlayerPrefs.Save();
    }

    // Llamado al pulsar “Opciones”
    public void OpenOptions()
    {
         Debug.Log("OpenOptions() INVOCADO");  // ← línea añadida
        if (panelOpciones != null)
            panelOpciones.SetActive(true);
    }

    // Llamado al pulsar “Volver” dentro de Opciones
    public void CloseOptions()
    {
        if (panelOpciones != null)
            panelOpciones.SetActive(false);
    }
}
