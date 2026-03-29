using TMPro;
using UnityEngine;

public class FinalScoreScript : MonoBehaviour
{
    private float finalScore;
    private TextMeshProUGUI finalScoreText;

    void Start()
    {
        finalScoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        finalScore = GameManager.instance.getScore();

        finalScoreText.text = "Final Score: " + Mathf.Round(finalScore).ToString();
    }
}
