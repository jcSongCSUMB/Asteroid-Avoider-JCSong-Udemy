using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private GameObject AsteroidSpawner;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    
    public void EndGame()
    {
        AsteroidSpawner.gameObject.SetActive(false);

        int finalScore = scoreSystem.EndTimer();
        gameOverText.text = $"Your score is {finalScore}";
        
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
