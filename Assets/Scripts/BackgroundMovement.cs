using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private float movementSpeed;
    [SerializeField] private float deadZone = -40f;

    void Update()
    {
        movementSpeed = GameManager.instance.movementSpeed / 2f;

        transform.position = transform.position + (Vector3.left * movementSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Skyscraper Background Destroyed");
            Destroy(gameObject);
        }
    }
}
