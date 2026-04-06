using System;
using UnityEngine;



// Canvas Render Mode: Screen Space - Overlay
// Overlay provides pixel-perfect UI anchored to the screen and is independent of camera transforms
// Canvas Scaler must be set to "Scale With Screen Size" with reference resolution 1920x1080.

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    void Awake()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Settings panel reference is not set in the inspector.");
        }
    }


    public void OnStartClicked()
    {
        // Load the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Metroid");
    }

    public void OnSettingsClicked()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
    }

    public void OnBackClicked()
    {
        // Hide the settings panel
        settingsPanel.SetActive(false);
    }

    public void OnExitClicked()
    {
        // Exit the application
        Application.Quit();
    }
}
