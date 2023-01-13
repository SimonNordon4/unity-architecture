using System.Collections;
using System.Collections.Generic;
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
    
    private void Start()
    {
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
        _pauseScreen.SetActive(false);
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
