using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float movementSpeed;
    [SerializeField] private float deadZone = -40f;

    void Update()
    {
        movementSpeed = GameManager.instance.movementSpeed;

        transform.position = transform.position + (Vector3.left * movementSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Obstacle Destroyed");
            Destroy(gameObject);
        }
    }
}
