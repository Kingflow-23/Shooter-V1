using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; 
    public Vector3 offset = new Vector3(0, 5, -10);

    void Start()
    {
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform);
    }
}
