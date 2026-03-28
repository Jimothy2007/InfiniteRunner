using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUIScript : MonoBehaviour
{
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private UIPanelFadeIn fadeInScript;

    void Awake()
    {
        if (deathScreenPanel == null)
        {
            Debug.LogError("Death Screen Panel is not assigned in the inspector.");
        }
    }

    void Start()
    {
        fadeInScript = deathScreenPanel.GetComponent<UIPanelFadeIn>();
        if (fadeInScript == null)
        {
            Debug.LogError("UIPanelFadeIn component is missing on the Death Screen Panel.");
        }

        deathScreenPanel.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        finalScoreText.text = "Final Score: " + Mathf.Round(GameManager.instance.getScore()).ToString();
        deathScreenPanel.SetActive(true);
        fadeInScript.FadeInPanel();
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
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
