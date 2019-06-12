using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public int currentScore;

    public int BestScore { get; private set; }

    public GameObject InGameCanvas;
    public GameObject StartScreenCanvas;
    public GameObject DeadCanvas;

    public GameObject flappyBird;

    //propertys that cannot be changed by other classes
    public static bool IsPaused{ get; private set; }

    private bool isGameStarted = false;
    public bool IsGameStarted
    {
        get { return isGameStarted; }
        set
        {
            GenerateScenario.isGameStarted = value;
            FlappyBirdController.isGameStarted = value;
            isGameStarted = value;
        }
    }

    void Awake()
    {
        IsPaused = false;
        instance = this;

        BestScore = PlayerPrefs.GetInt("BestScore");
    }

    public void Home()
    {
        IsGameStarted = false;
        GenerateScenario.instance.CleanAllBackground();
        flappyBird.GetComponent<FlappyBirdController>().Start();

        DeadCanvas.SetActive(false);
        InGameCanvas.SetActive(false);
        StartScreenCanvas.SetActive(true);

        flappyBird.transform.parent.position = new Vector3(0, -0.22f, -10);
    }


    public void StartGame()
    {
        InGameCanvas.transform.Find("ScoreTextWhite").GetComponent<Text>().text = currentScore.ToString();
        StartScreenCanvas.SetActive(false);
        DeadCanvas.SetActive(false);
        InGameCanvas.SetActive(true);

        IsGameStarted = true;
    }

    public void AddScorePoint()
    {
        currentScore++;
        InGameCanvas.transform.Find("ScoreTextWhite").GetComponent<Text>().text = currentScore.ToString();
    }

    public void Pause()
    {
        if (IsPaused)
        {
            IsPaused = false;
            flappyBird.GetComponent<FlappyBirdController>().Pause(false);

        }
        else
        {
            IsPaused = true;
            flappyBird.GetComponent<FlappyBirdController>().Pause(true);
        }
    }

    public void Die()
    {
        InGameCanvas.SetActive(false);
        DeadCanvas.SetActive(true);

        if (currentScore > BestScore)
            PlayerPrefs.SetInt("BestScore", currentScore);

        BestScore = PlayerPrefs.GetInt("BestScore");
        DeadCanvas.GetComponent<DeadCanvas_Manager>().SetScoreValues(currentScore, BestScore);

        currentScore = 0;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
