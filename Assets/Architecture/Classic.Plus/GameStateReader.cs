using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Architecture.Classic.Plus
{
    public class GameStateReader : MonoBehaviour
    {
        private GameState _gameStateReference;
        private GameState.State _lastFrameState = GameState.State.None;

        [Tooltip("List of behaviours that should only be enabled when the state is active.")]
        [SerializeField]
        private List<MonoBehaviour> activeStateBehaviours = new List<MonoBehaviour>();
        
        private void Start()
        {
            _gameStateReference = GameCatalog.Instance.GameState;
        }

        private void Update()
        {
            var state = _gameStateReference.CurrentState;
            if (state == _lastFrameState)
                return;

            switch (state)
            {
                case GameState.State.None:
                    DisableActiveState();
                    break;
                case GameState.State.Active:
                    EnabledActiveState();
                    break;
                case GameState.State.Paused:
                    DisableActiveState();
                    break;
                case GameState.State.Finished:
                    DisableActiveState();
                    break;
            }

            _lastFrameState = state;
        }

        private void EnabledActiveState()
        {
            foreach(var behaviour in activeStateBehaviours)
                behaviour.enabled = true;
        }

        private void DisableActiveState()
        {
            foreach(var behaviour in activeStateBehaviours)
                behaviour.enabled = false;
        }
    }
}