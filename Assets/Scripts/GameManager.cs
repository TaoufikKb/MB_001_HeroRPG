using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlaying { get; private set; }

    [SerializeField] EnemiesSpawner _enemiesSpawner;
    [SerializeField] Level _level;

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

    public void RestartGame()
    {
        DOTween.KillAll();

        _player.ResetStats();

        _enemiesSpawner.ClearAllEnemies();
        _level.ClearLevel();
    }

    public void EndGame()
    {
        isPlaying = false;

        _uiManager.ShowEndGameUI(true);
    }
}
