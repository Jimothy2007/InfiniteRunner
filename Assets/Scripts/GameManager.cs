using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private GameObject pauseScreenPanel;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    private float score = 0;
    public float movementSpeed = 4f;
    [SerializeField] private float difficultyScale = 0.1f;
    private bool isGameOver = false;

    [SerializeField] private AudioSource increasedScore;
    [SerializeField] private AudioSource backgroundMusic1;
    [SerializeField] private AudioSource backgroundMusic2;
    [SerializeField] private AudioSource deathSound;

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

        StartCoroutine(MusicLoop());
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

            heart1 = GameObject.Find("Heart 1");
            heart2 = GameObject.Find("Heart 2");
            heart3 = GameObject.Find("Heart 3");
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

        if (score > 0 && Mathf.FloorToInt(score) % 1000 == 0)
        {
            if (!increasedScore.isPlaying)
            {
                increasedScore.Play();
            }
        }
    }

    public void Death()
    {
        if (isGameOver) return;

        isGameOver = true;
        backgroundMusic1.Stop();
        backgroundMusic2.Stop();
        deathSound.Play();
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

    private IEnumerator MusicLoop()
    {
        float fadeDuration = 4f;

        backgroundMusic1.volume = 1f;
        backgroundMusic2.volume = 0f;

        backgroundMusic1.Play();

        while (!isGameOver)
        {
            yield return new WaitForSeconds(backgroundMusic1.clip.length - fadeDuration);

            backgroundMusic2.Play();

            StartCoroutine(Fade(backgroundMusic1, 0f, fadeDuration));
            StartCoroutine(Fade(backgroundMusic2, 0.75f, fadeDuration));

            yield return new WaitForSeconds(fadeDuration);

            backgroundMusic1.Stop();

            yield return new WaitForSeconds(backgroundMusic2.clip.length - fadeDuration);

            backgroundMusic1.Play();

            StartCoroutine(Fade(backgroundMusic1, 1f, fadeDuration));
            StartCoroutine(Fade(backgroundMusic2, 0f, fadeDuration));

            yield return new WaitForSeconds(fadeDuration);

            backgroundMusic2.Stop();
        }
    }

    private IEnumerator Fade(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    public void UpdateHearts(int health)
    {
        heart1.SetActive(health >= 3);
        heart2.SetActive(health >= 2);
        heart3.SetActive(health >= 1);
    }
}
