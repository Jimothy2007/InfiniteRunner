using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject playerPrefab;
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
        isGameOver = true;
        DeathScreenUIScript.instance.ShowDeathScreen();
    }

    public float getScore()
    {
        return score;
    }
}
