using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private float displayedScore = 0f;
    [SerializeField] private float scoreUpdateSpeed = 50f;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float actualScore = GameManager.instance.getScore();
        scoreUpdateSpeed = GameManager.instance.movementSpeed * 200f;

        displayedScore += scoreUpdateSpeed * Time.deltaTime;

        displayedScore = Mathf.Min(displayedScore, actualScore);

        scoreText.text = "Score: " + Mathf.FloorToInt(displayedScore).ToString();
    }
}
