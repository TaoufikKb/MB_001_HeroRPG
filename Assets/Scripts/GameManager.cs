using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlaying { get; private set; }

    Player _player;
    UiManager _uiManager;

    void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        Application.targetFrameRate = 60;

        _uiManager = UiManager.instance;
        _player = Player.instance;
    }


    public void StartGame()
    {
        isPlaying = true;
    }

    public void ContinueGame()
    {
        isPlaying = true;
        _player.Revive();
    }

    public void EndGame()
    {
        isPlaying = false;

        _uiManager.ShowEndGameUI(true);
    }
}
