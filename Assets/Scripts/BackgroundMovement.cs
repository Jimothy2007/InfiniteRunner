using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float deadZone = -40f;

    void Update()
    {
        transform.position = transform.position + (Vector3.left * movementSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Skyscraper Background Destroyed");
            Destroy(gameObject);
        }
    }
}
