using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private float baseDistance = 40f;

    private GameObject lastBackground;
    void Start()
    {
        spawnBackground();
    }

    void Update()
    {
        var movementSpeed = GameManager.instance.movementSpeed / 2f;

        if (lastBackground != null)
        {
            var distanceSinceLast = lastBackground.transform.position.x - transform.position.x;
            if (distanceSinceLast <= -baseDistance)
            {
                spawnBackground();
            }
        }
    }

    void spawnBackground()
    {
        Vector2 spawnPosition = transform.position;
        if (lastBackground != null)
        {
            spawnPosition.x = lastBackground.transform.position.x + baseDistance;
        }

        lastBackground = Instantiate(backgroundPrefab, spawnPosition, Quaternion.identity);
    }
}
