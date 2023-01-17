﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture.Classic.Events
{
    /// <summary>
    /// In this example we've transformed the GameInterface into a Singleton. Making it globally accessible.
    /// It's generally okay to make a Singleton of an object when there's only ever meant to be a single instance.
    /// We could also have made the Player a Singleton as well, however if have other objects access the Player
    /// through the Game Manager Singleton, it means we have a central source of global level state.
    /// </summary>
    public class GameInterface : MonoBehaviour
    {
        public event Action OnGameWin;
        public event Action OnGameLose;
        public event Action OnGamePause;
        public event Action OnGameResume;
        
        // reference the current state
        private GameState _state;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void Start()
        {
            _state = GameCatalog.Instance.GameState;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_state.CurrentState == GameState.State.Active)
                {
                    PauseGame();
                    return;
                }

                if (_state.CurrentState == GameState.State.Paused)
                {
                    ResumeGame();
                    return;
                }
            }
        }

        public void OnStartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ResumeGame()
        {
            OnGameResume?.Invoke();
        }
        
        public void PauseGame()
        {
            OnGamePause?.Invoke();
        }

        public void WinGame()
        {
            OnGameWin?.Invoke();
        }

        public void LoseGame()
        {
            OnGameLose?.Invoke();
        }
    }
}