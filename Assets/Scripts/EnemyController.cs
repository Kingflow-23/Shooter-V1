using TMPro;
using UnityEngine;
using StarterAssets;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 3.0f; // Speed of the enemy 
    public float rotationSpeed = 5.0f; // Speed of the rotation
    private GameObject gameOverSign;
    private GameObject gameMaster; 
    public GameObject ImpactEffectPrefab; 
    public AudioClip gameOverSound;
    private AudioSource audioSource;
    private TMP_Text textMeshPro;
    private Rigidbody rb;

    private void Awake()
    {
        gameOverSign = GameObject.FindGameObjectWithTag("GameOver");
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster"); // Find the GameMaster object
    }

    void Start()
    {
        // Find PlayerController in the scene
        ThirdPersonController player = Object.FindFirstObjectByType<ThirdPersonController>();

        // Check if PlayerController exists
        if (player != null)
        {
            playerTransform = player.transform;  // Only assign if found
        }
        else
        {
            Debug.Log("PlayerController Died");
        }

        // Initialize game over text and set it to inactive
        textMeshPro = gameOverSign.GetComponent<TMP_Text>();
        textMeshPro.enabled = false;

        // Initialize audio source
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 target = playerTransform.position;
        Vector3 newPosition = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Rotate towards the player
        Vector3 direction = playerTransform.position - rb.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        // Move towards the player
    //    transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

        // Rotate towards the player
    //    Vector3 direction = playerTransform.position - transform.position;
    //    direction.y = 0; // Keep it flat

        // Only rotate if direction is not zero
    //    if (direction != Vector3.zero)
    //    {
    //        Quaternion rotation = Quaternion.LookRotation(direction);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        if (null == other.GetComponent<ThirdPersonController>())
        {
            return;
        }

        // Handle collision with the player (e.g., reduce health, game over, etc.)
        Debug.Log("Enemy collided with player!");

        // Play impact effect
        Instantiate(ImpactEffectPrefab, transform.position, Quaternion.identity);

        // Game over logic
        gameMaster.GetComponent<GameMaster>().GameOver();

        // Play game over sound
        PlayGameOverSound();

        // Destroy the player and enemy objects
        // Destroy(gameObject);

        Time.timeScale = 0f; // Pause the game
    }

    private void PlayGameOverSound()
    {
        // Create a temporary audio source and play the game over sound
        GameObject soundObject = new GameObject("GameOverSound");
        AudioSource tempAudioSource = soundObject.AddComponent<AudioSource>();
        tempAudioSource.clip = gameOverSound;
        tempAudioSource.Play();

        // Destroy the sound object after the sound finishes playing
        Destroy(soundObject, gameOverSound.length);
    }
}

