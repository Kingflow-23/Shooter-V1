using TMPro; 
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public AudioClip shootSound;
    private AudioSource audioSource;
    public Transform playerTransform;
    private GameObject groundPlane; 
    private TextMeshProUGUI scoreText; 
    public GameObject bulletPrefab;
    public GameObject smokePrefab;
    public float speed = 10f;
    private float jumpForce = 7.5f;
    private int score = 0;
    private bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        playerTransform.position = new Vector3(0, 1.5f, 0);

        rb = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>();
        groundPlane = GameObject.FindGameObjectWithTag("Ground");
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player Position: " + playerTransform.position);
    
        // Movement logic
        if (Input.GetKey(KeyCode.W))
        {
            playerTransform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerTransform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerTransform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTransform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Rotation logic
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            playerTransform.LookAt(targetPosition);
        }

        // Shooting logic
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, playerTransform.position + playerTransform.forward, playerTransform.rotation);
        
            if (shootSound != null)
                audioSource.PlayOneShot(shootSound);

            // Instantiate explosion effect
            Instantiate(smokePrefab, transform.position, Quaternion.identity);
        }
    }

    void Jump()
    {
        // Apply an upward impulse to make the player jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    
        //if (jumpSound != null)
        //    audioSource.PlayOneShot(jumpSound);
    }

    // Set isGrounded to true when colliding with the specified groundPlane
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Set isGrounded to false when leaving collision with the specified groundPlane
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Method to increase score
    public void UpdateScore(int increment)
    {
        score += increment;

        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }
    }

    // Reset the score to zero
    public void ResetScore()
    {
        score = 0;
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}
