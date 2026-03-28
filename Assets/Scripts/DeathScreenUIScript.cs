using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUIScript : MonoBehaviour
{
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public static DeathScreenUIScript instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        deathScreenPanel.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        finalScoreText.text = "Final Score: " + Mathf.Round(GameManager.instance.getScore()).ToString();
        deathScreenPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
