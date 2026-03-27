using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab1;
    [SerializeField] private GameObject groundPrefab2;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float spawnRate = 4f;
    [SerializeField] private float timer = 0f;
    void Start()
    {
        spawnGround();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnGround();
            timer = 0;
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

        Instantiate(randomPrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z), Quaternion.identity);
        spawnObstacle(randomPrefab);
    }

    void spawnObstacle(GameObject groundPrefab)
    {
        if (groundPrefab == groundPrefab1)
        {
            //var randomObstacleSpawn = 
        }
    }
}
