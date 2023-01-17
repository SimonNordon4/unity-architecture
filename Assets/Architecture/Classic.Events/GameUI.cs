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
        GameCatalog.Instance.GameInterface.OnGameLose += ShowLoseScreen;
        GameCatalog.Instance.GameInterface.OnGameWin += ShowWinScreen;
        GameCatalog.Instance.GameInterface.OnGamePause += ShowPauseScreen;
        GameCatalog.Instance.GameInterface.OnGameResume += HidePauseScreen;

        GameCatalog.Instance.Player.GetComponent<Gun>().GunCooldownChanged += UpdateGunCoolDownText;
        GameCatalog.Instance.EnemySpawner.OnEnemiesLeftChanged += UpdateEnemiesRemainingText;
    }


    private void OnDisable()
    {
        GameCatalog.Instance.GameInterface.OnGameLose -= ShowLoseScreen;
        GameCatalog.Instance.GameInterface.OnGameWin -= ShowWinScreen;
        GameCatalog.Instance.GameInterface.OnGamePause -= ShowPauseScreen;
        GameCatalog.Instance.GameInterface.OnGameResume -= HidePauseScreen;
        
        GameCatalog.Instance.Player.GetComponent<Gun>().GunCooldownChanged -= UpdateGunCoolDownText;
        GameCatalog.Instance.EnemySpawner.OnEnemiesLeftChanged -= UpdateEnemiesRemainingText;
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

    public void UpdateLevelText(int level)
    {
        _levelText.text = "Level: " + level;
    }
    
    public void UpdateGunCoolDownText(float coolDown)
    {
        _gunCoolDownText.text = "Gun Cool Down: " + coolDown.ToString("F2");
    }
    
    public void UpdateEnemiesRemainingText(int enemiesLeft)
    {
        _enemiesRemainingText.text = "Enemies Remaining: " + enemiesLeft;
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
