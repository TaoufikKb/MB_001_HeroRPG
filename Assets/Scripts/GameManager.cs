using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int points { get; private set; }
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

        SetPoints(0);
    }


    public void StartGame()
    {
        isPlaying = true;
    }

    public void ContinueGame()
    {
        isPlaying = true;
        _enemiesSpawner.KillAllEnemies();

        DOVirtual.DelayedCall(1, _player.Revive);
    }

    public void RestartGame()
    {
        DOTween.KillAll();

        _player.ResetStats();

        _enemiesSpawner.ClearAllEnemies();
        _level.ClearLevel();

        SetPoints(0);
    }

    public void EndGame()
    {
        isPlaying = false;

        _uiManager.ShowEndGameUI(true);
    }

    public void AddPoints(int add)
    {
        SetPoints(points + add);
    }

    void SetPoints(int points)
    {
        this.points = points;
        _uiManager.UpdatePointsUI(points);
    }
}
