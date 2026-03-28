using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab;
    private float spawnRate = 5f;
    [SerializeField] private float baseDistance = 50f;
    [SerializeField] private float timer = 0f;
    void Start()
    {
        spawnBackground();
    }

    void Update()
    {
        var movementSpeed = GameManager.instance.movementSpeed / 2f;

        spawnRate = baseDistance / movementSpeed;

        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnBackground();
            timer = 0;
        }
    }

    void spawnBackground()
    {
        Instantiate(backgroundPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}
