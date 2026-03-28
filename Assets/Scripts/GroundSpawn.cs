using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab1;
    [SerializeField] private GameObject groundPrefab2;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float baseDistance = 10f;

    private GameObject lastGround;

    void Start()
    {
        spawnGround();
    }

    void Update()
    {
        if (lastGround != null)
        {
            float movementSpeed = GameManager.instance.movementSpeed;

            if (lastGround.transform.position.x <= transform.position.x)
            {
                spawnGround();
            }
        }
    }

    void spawnGround()
    {
        var randomPrefab = Random.Range(0, 2) == 0 ? groundPrefab1 : groundPrefab2;
        var groundHeightOffset = randomPrefab.GetComponent<GroundMovement>().getHeightOffset();

        float lowestPoint = transform.position.y - groundHeightOffset;
        float highestPoint = transform.position.y + groundHeightOffset;

        if (randomPrefab == groundPrefab2)
        {
            highestPoint -= 0.25f;
        }

        var spawnX = transform.position.x;
        if (lastGround != null)
        {
            spawnX = lastGround.transform.position.x + baseDistance;
        }

        Vector2 spawnPosition = new Vector2(spawnX, Random.Range(lowestPoint, highestPoint));
        lastGround = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

        spawnObstacle(lastGround);
    }

    void spawnObstacle(GameObject groundInstance)
    {
        int childCount = groundInstance.transform.childCount;

        var randomObstacleSpawn = Random.Range(0, childCount);

        Transform spawnPoint = groundInstance.transform.GetChild(randomObstacleSpawn);

        Instantiate(obstacle, new Vector3(spawnPoint.position.x, spawnPoint.position.y, transform.position.z), Quaternion.identity);

        Debug.Log("Spawned obstacle at: " + spawnPoint.position);
    }
}
