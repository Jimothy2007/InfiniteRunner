using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float deadZone = -40f;

    void Update()
    {
        transform.position = transform.position + (Vector3.left * movementSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Ground Destroyed");
            Destroy(gameObject);
        }
    }
}
