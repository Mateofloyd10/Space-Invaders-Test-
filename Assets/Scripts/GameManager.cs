using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject opening;
    public GameObject playerShip;
    public GameObject gamePlay;
    public GameObject gameOver;
    public GameObject credits;
    public GameObject youWin;
    public GameObject nextLevel;   
    public GameObject[] gameLevels;
    public AudioSource backGroundSound;
    public int levelNumber;
    public int enemiesNumber;

    private GameObject levelToPlay;
    private Animator creditsAnimation;
    public enum GameManagerState
    {
        Opening,
        GamePlay,
        NextLevel,
        GameOver,
        Credits,
    }


    GameManagerState GMState;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GMState = GameManagerState.Opening;
        creditsAnimation = credits.transform.GetComponentInChildren<Animator>();
        UpdateGameManagerState();
    }
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                backGroundSound.Play();
                opening.SetActive(true);
                gamePlay.SetActive(false);
                gameOver.SetActive(false);
                credits.SetActive(false);
                youWin.SetActive(false);

                break;

            case GameManagerState.GamePlay:
                CreateLevel();
                opening.SetActive(false);
                gamePlay.SetActive(true);
                gameOver.SetActive(false);
                youWin.SetActive(false);
                playerShip.GetComponent<PlayerController>().Init();
                break;
            case GameManagerState.NextLevel:
                opening.SetActive(false);
                gameOver.SetActive(false);
                gamePlay.SetActive(false);
                youWin.SetActive(true);
                break;

            case GameManagerState.GameOver:
                Destroy(levelToPlay);
                opening.SetActive(false);
                gameOver.SetActive(true);
                gamePlay.SetActive(false);
                youWin.SetActive(false);
                backGroundSound.Stop();
                break;

            case GameManagerState.Credits:
                credits.SetActive(true);
                creditsAnimation.Play("CreditsAnim");
                gameOver.SetActive(false);
                opening.SetActive(false);
                youWin.SetActive(false);
                backGroundSound.Stop();
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.GamePlay;
        UpdateGameManagerState();
    }
    public void levelToSet(string levelDifficult)
    {
        
        switch (levelDifficult.ToLower())
        {
            case"easy":
                levelNumber = 0;
                break;
            case"normal":
                levelNumber = 1;
                break;
            case"hard":            
                levelNumber = 2;
                break;
            case null:
                Debug.Log("Escribir bien el nombre");
                break;
                
        }
       
    }

    private void CreateLevel()
    {
        levelToPlay = Instantiate(gameLevels[levelNumber]);
        enemiesNumber = levelToPlay.GetComponentsInChildren<Transform>().Length - 1;
        
        if (levelNumber == gameLevels.Length - 1)
            nextLevel.SetActive(false);

        else
            nextLevel.SetActive(true);
    }

    public void NextLevel()
    {
        if (levelNumber < gameLevels.Length - 1)
            levelNumber++;
    }
   
}
