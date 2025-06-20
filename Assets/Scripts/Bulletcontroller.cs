using UnityEngine;
using StarterAssets;

public class BulletController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject ImpactPrefab;
    public AudioClip impactSound;
    private AudioSource audioSource;
    public float speed = 20f; 
    public float lifetime = 5f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Get the Renderer component and set a random color
        Renderer bulletRenderer = GetComponent<Renderer>();
        if (bulletRenderer != null)
        {
            // Create a random color with full alpha (1)
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1f);
            bulletRenderer.material.color = randomColor;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    
        // Destroy the bullet after a certain time
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy == null)
        {
            return;
        }

        // Play impact sound
        PlayImpactSound();

        // Instantiate impact effect at the collision point
        Instantiate(ImpactPrefab, transform.position, Quaternion.identity);

        // Update score 
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            ThirdPersonController playerController = playerObj.GetComponent<ThirdPersonController>();
            if (playerController != null)
            {
                playerController.UpdateScore(1);
            }
        }

        // Destroy the enemy and the bullet on collision
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    
    private void PlayImpactSound()
    {
        // Create a temporary audio source and play the impact sound
        GameObject soundObject = new GameObject("ImpactSound");
        AudioSource tempAudioSource = soundObject.AddComponent<AudioSource>();
        tempAudioSource.clip = impactSound;
        tempAudioSource.Play();

        // Destroy the sound object after the sound finishes playing
        Destroy(soundObject, impactSound.length);
    }
}
