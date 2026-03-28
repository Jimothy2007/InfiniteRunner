using UnityEngine;

public class TitleTiltScript : MonoBehaviour
{
    [SerializeField] private float tiltAmount = 5f;
    [SerializeField] private float tiltSpeed = 1f;

    void Update()
    {
        float angle = Mathf.PingPong(Time.time * tiltSpeed, tiltAmount * 2) - tiltAmount;

        transform.localEulerAngles = new Vector3(0f, 0f, angle);
    }
}
