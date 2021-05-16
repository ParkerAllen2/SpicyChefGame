using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    protected GameController() { }
    public bool gameOver;

    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameOver = false;
    }

    public void LoadScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
