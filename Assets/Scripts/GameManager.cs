using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isGameStarted;
    public void StartGame()
    {
        if (isGameStarted) return;
        isGameStarted = true;
        BallController.Instance.StartGame();
       
    }

    public void GameOver() 
    {
        GameOverPanel.Instance.Open();
        
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void LevelCompleted() 
    {
    
    }


}
