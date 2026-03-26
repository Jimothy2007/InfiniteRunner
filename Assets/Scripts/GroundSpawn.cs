using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float heightOffset = 10f;
    void Start()
    {
        spawnGround();
    }

    // Update is called once per frame
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
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(groundPrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z), Quaternion.identity);
    }
}
