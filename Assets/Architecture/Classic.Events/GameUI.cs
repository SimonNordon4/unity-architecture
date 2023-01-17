using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.Classic.Events;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    
    
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _pauseScreen;

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _gunCoolDownText;
    [SerializeField] private TextMeshProUGUI _enemiesRemainingText;
    
    private EnemySpawner _enemySpawner;
    private Gun _gun;

    private void OnEnable()
    {
        GameInterface.OnGameLose += ShowLoseScreen;
        GameInterface.OnGameWin += ShowWinScreen;
        GameInterface.OnGamePause += ShowPauseScreen;
        GameInterface.OnGameResume += HidePauseScreen;
    }
    
    private void OnDisable()
    {
        GameInterface.OnGameLose -= ShowLoseScreen;
        GameInterface.OnGameWin -= ShowWinScreen;
        GameInterface.OnGamePause -= ShowPauseScreen;
        GameInterface.OnGameResume -= HidePauseScreen;
    }

    private void Start()
    {
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
        _pauseScreen.SetActive(false);
        
        _enemySpawner = GameCatalog.Instance.EnemySpawner;
        _gun = GameCatalog.Instance.Player.GetComponent<Gun>();
        UpdateLevelText(1);
    }

    private void Update()
    {
        UpdateGunCoolDownText(_gun.BulletCooldown);
        UpdateEnemiesRemainingText(_enemySpawner.CurrentEnemies + _enemySpawner.EnemiesToSpawn);
    }

    public void UpdateLevelText(int level)
    {
        _levelText.text = "Level: " + level;
    }
    
    public void UpdateGunCoolDownText(float coolDown)
    {
        _gunCoolDownText.text = "Gun Cool Down: " + coolDown;
    }
    
    public void UpdateEnemiesRemainingText(int enemiesRemaining)
    {
        _enemiesRemainingText.text = "Enemies Remaining: " + enemiesRemaining;
    }
    
    public void ShowWinScreen()
    {
        _winScreen.SetActive(true);
    }
    
    public void ShowLoseScreen()
    {
        _loseScreen.SetActive(true);
    }
    
    public void ShowPauseScreen()
    {
        _pauseScreen.SetActive(true);
    }
    
    public void HidePauseScreen()
    {
        _pauseScreen.SetActive(false);
    }
}
