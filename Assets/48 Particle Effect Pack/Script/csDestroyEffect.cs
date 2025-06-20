using UnityEngine;

public class csDestroyEffect : MonoBehaviour
{
    void Start()
    {
        // Automatically destroy this GameObject after 1 second
        Destroy(gameObject, 0.5f);
    }
}