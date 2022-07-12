using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    private string sceneName;
    private Animator animatorController;
    [SerializeField]private GameScore gameScore;
    void Start()
    {
        animatorController = GetComponent<Animator>();
      
    }

    public void PlayFade(string changeScene)
    {
        sceneName = changeScene;
        animatorController.Play("FadeAnimation");
    }

    public void LoadScene()
    {
        switch (sceneName)
        {
            case "GamePlay":
                GameManager.instance.SetGameManagerState(GameManager.GameManagerState.GamePlay);          
                Invoke("ResetScore", .1f);
                break;

            case"Opening":
                GameManager.instance.SetGameManagerState(GameManager.GameManagerState.Opening);
                break;

            case "NextLevel":
                GameManager.instance.SetGameManagerState(GameManager.GameManagerState.Opening);
                break;

            case "Credits":
                GameManager.instance.SetGameManagerState(GameManager.GameManagerState.Credits);
                break;
        }

       
    }

    public void ResetScore() 
    {
        gameScore.Score = 0;
    }
    void Update()
    {
        
    }
}
