using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    private float movementSpeed;
    [SerializeField] private float deadZone = -40f;
    [SerializeField] float heightOffset = 1.75f;

    void Update()
    {
        movementSpeed = GameManager.instance.movementSpeed;

        transform.position += (Vector3.left * movementSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Ground Destroyed");
            Destroy(gameObject);
        }
    }

    public float getHeightOffset()
    {
        return heightOffset;
    }
}
