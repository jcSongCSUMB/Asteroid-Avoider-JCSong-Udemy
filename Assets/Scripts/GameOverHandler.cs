using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private GameObject AsteroidSpawner;
    
    public void EndGame()
    {
        AsteroidSpawner.gameObject.SetActive(false);
        gameOverDisplay.gameObject.SetActive(true);
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
