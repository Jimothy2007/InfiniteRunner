using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private GameObject pauseScreenPanel;
    private float score = 0;
    public float movementSpeed = 4f;
    [SerializeField] private float difficultyScale = 0.1f;
    private bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        var deathUIScript = FindFirstObjectByType<DeathScreenUIScript>();
        if (deathUIScript != null)
        {
            deathScreenPanel = deathUIScript.gameObject;
        }

        var pauseUIScript = FindFirstObjectByType<PauseScreenUIScript>();
        if (pauseUIScript != null)
        {
            pauseScreenPanel = pauseUIScript.gameObject;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            score = 0;
            movementSpeed = 4f;
            isGameOver = false;
            SpawnPlayer();

            var deathUIScript = FindFirstObjectByType<DeathScreenUIScript>();
            if (deathUIScript != null)
            {
                deathScreenPanel = deathUIScript.gameObject;
            }

            var pauseUIScript = FindFirstObjectByType<PauseScreenUIScript>();
            if (pauseUIScript != null)
            {
                pauseScreenPanel = pauseUIScript.gameObject;
            }
        }
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, new Vector3(-5f, 0f, 0f), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player prefab is not assigned in the GameManager.");
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            movementSpeed += Time.deltaTime * difficultyScale;
        }
        else if (isGameOver)
        {
            movementSpeed = 0f;
        }

        if (!isGameOver)
        {
            score += (movementSpeed * 10) * Time.deltaTime;
        }
    }

    public void Death()
    {
        if (isGameOver) return;

        isGameOver = true;
        deathScreenPanel.GetComponent<DeathScreenUIScript>().ShowDeathScreen();
    }

    public float getScore()
    {
        return score;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreenPanel.GetComponent<PauseScreenUIScript>().ShowPauseScreen();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseScreenPanel.SetActive(false);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TitleScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}
