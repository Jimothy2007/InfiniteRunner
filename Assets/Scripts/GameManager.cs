using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject playerPrefab;
    private int score = 0;
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
    void Start()
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
        movementSpeed += Time.deltaTime * difficultyScale;

        if (!isGameOver)
        {
            score += Mathf.RoundToInt(Time.deltaTime * 10);
        }
    }

    public void Death()
    {
        isGameOver = true;
    }
}
