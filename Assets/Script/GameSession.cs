using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    int PlayerLives = 3;
    [SerializeField] int Score = 0;

    [SerializeField] TextMeshProUGUI LivesText;
    [SerializeField] TextMeshProUGUI ScoreText;
    void Awake()
    {
        int NumGameSessions = FindObjectsOfType<GameSession>().Length;
        if (NumGameSessions > 1 )
        {
            Destroy( gameObject );
        }
        else
        {
            DontDestroyOnLoad( gameObject );
        }
    }

    private void Start()
    {
        LivesText.text = PlayerLives.ToString();
        ScoreText.text = Score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(PlayerLives >1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy( gameObject );
    }

    void TakeLife()
    {
        PlayerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        LivesText.text = PlayerLives.ToString();

    }

    public void AddToScore(int PointsToAdd)
    {
        Score += PointsToAdd;
        ScoreText.text = Score.ToString();
    }
}
