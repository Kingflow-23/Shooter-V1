using TMPro;
using UnityEngine;
using StarterAssets;

public class GameMaster : MonoBehaviour
{
    public GameObject playerPrefab; 
    private GameObject currentPlayer;
    private GameObject gameOverSign;
    private GameObject restartButton; 
    public GameObject scoreUI; 

    // private PlayerController playerController;

    private ThirdPersonController playerController; 
    private TMP_Text textMeshPro;

    private void Awake()
    {
        gameOverSign = GameObject.FindGameObjectWithTag("GameOver");
        restartButton = GameObject.FindGameObjectWithTag("RestartButton");
    }

    void Start()
    {
        // Instantiate the player at a fixed starting position
        Vector3 startPosition = new Vector3(0, 1.5f, 0);
        currentPlayer = Instantiate(playerPrefab, startPosition, Quaternion.identity);

        playerController = currentPlayer.GetComponent<ThirdPersonController>();
    
        // Initialize game over text and set it to inactive
        textMeshPro = gameOverSign.GetComponent<TMP_Text>();
        textMeshPro.enabled = false;

        restartButton.SetActive(false);
    }

    public void GameOver()
    {
        // Display Game Over message
        textMeshPro.enabled = true;
        restartButton.SetActive(true);

        // Deactivate the player
        currentPlayer.SetActive(false);
        
        // Pause the game 
        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        
        // Destroy all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Reset the player
        currentPlayer.SetActive(true); 
        currentPlayer.transform.position = new Vector3(0, 1.5f, 0);

        // Reconnect the new PlayerController
        playerController = currentPlayer.GetComponent<ThirdPersonController>();

        // Reset the score
        playerController.ResetScore();

        // Hide game over sign
        textMeshPro.enabled = false;
        restartButton.SetActive(false);
    }
}
