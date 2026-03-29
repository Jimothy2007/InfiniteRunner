using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenUIScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreenPanel;

    private UIPanelFadeIn fadeInScript;

    void Awake()
    {
        if (pauseScreenPanel == null)
        {
            Debug.LogError("Pause Screen Panel is not assigned in the inspector.");
        }
    }

    void Start()
    {
        fadeInScript = pauseScreenPanel.GetComponent<UIPanelFadeIn>();
        if (fadeInScript == null)
        {
            Debug.LogError("UIPanelFadeIn component is missing on the Death Screen Panel.");
        }

        pauseScreenPanel.SetActive(false);
    }

    public void ShowPauseScreen()
    {
        pauseScreenPanel.SetActive(true);
        fadeInScript.FadeInPanel();
    }

    public void ResumeGame()
    {
        pauseScreenPanel.SetActive(false);
        GameManager.instance.ResumeGame();
    }

    public void TitleScreen()
    {
        GameManager.instance.TitleScreen();
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}