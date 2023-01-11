﻿using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;

namespace Architecture.Classic.Plus
{
    /// <summary>
    /// Monitors and controls the state of the gameplay level.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // Create a Game Manager Instance.
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        }
        
        // Game State.
        public enum GameState
        {
            None,
            Paused,
            Active,
            GameOver
        }
        
        [field: SerializeField]
        public GameState CurrentGameState { get; private set; }
        
        // public Health playerHealth;
        [field: SerializeField]
        public Transform PlayerTransform { get; private set; }
        
        [field: SerializeField]
        public EnemySpawner EnemySpawner { get; private set; }

        [SerializeField]
        private GameObject winScreen = null;
        [SerializeField]
        private GameObject loseScreen = null;

        private void Start()
        {
            if (PlayerTransform is null)
                throw new NullReferenceException("Player Transform is null On GameManager");
            if (EnemySpawner is null)
                throw new NullReferenceException("Enemy Spawn is null on GameManager");
            if (winScreen is null)
                throw new NullReferenceException("Win Screen is null on GameManager");
            if (loseScreen is null)
                throw new NullReferenceException("Lose Screen is null on GameManager");
        }

        public void StartGame()
        {
            // TODO: Reset Variables
            CurrentGameState = GameState.Active;
        }

        public void ResumeGame()
        {
            CurrentGameState = GameState.Active;
        }
        
        public void PauseGame()
        {
            CurrentGameState = GameState.Paused;
        }

        public void WinGame()
        {
            CurrentGameState = GameState.GameOver;
            winScreen.SetActive(true);
        }

        public void LoseGame()
        {
            CurrentGameState = GameState.GameOver;
            loseScreen.SetActive(false);
        }
    }
}