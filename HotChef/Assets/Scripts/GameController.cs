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

    public void Exit()
    {
        Application.Quit();
    }
}
