using UnityEngine;

public class BoardColorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Generate a random color with random RGB values between 0 and 1
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        // Assuming the object has a Renderer component (like a Cube, Plane, etc.)
        Renderer renderer = GetComponent<Renderer>();

        // Check if the Renderer exists and set the material's color
        if (renderer != null)
        {
            renderer.material.color = randomColor;
        }
        else
        {
            Debug.LogWarning("Renderer not found on this object.");
        }
    }
}
