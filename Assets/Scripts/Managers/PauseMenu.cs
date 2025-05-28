using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;   // para StarterAssetsInputs
using StarterAssets;   // para ThirdPersonController

public class PauseMenu : MonoBehaviour
{
    [Header("Referencias UI")]
    [Tooltip("El panel semitransparente que cubre toda la pantalla")]
    [SerializeField] private GameObject pausePanel;

    [Header("Escena de Menú Principal")]
    [Tooltip("Nombre exacto de tu escena de menú")]
    [SerializeField] private string mainMenuSceneName = "MenuPrincipal";

    [Header("Scripts a desactivar al pausar")]
    [Tooltip("Arrastra aquí tu ThirdPersonController y StarterAssetsInputs")]
    [SerializeField] private MonoBehaviour[] scriptsToDisable;

    private bool isPaused;

    private void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel.SetActive(false);

        // Asegura cursor oculto y bloqueado al inicio de la partida
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);

        // Mostrar y desbloquear cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;

        // Desactivar scripts de cámara/input
        foreach (var comp in scriptsToDisable)
            if (comp != null)
                comp.enabled = false;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        // Ocultar y volver a bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;

        // Reactivar scripts
        foreach (var comp in scriptsToDisable)
            if (comp != null)
                comp.enabled = true;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
