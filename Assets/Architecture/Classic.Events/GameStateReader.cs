using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Architecture.Classic.Events
{
    public class GameStateReader : MonoBehaviour
    {
        [Tooltip("List of behaviours that should only be enabled when the state is active.")]
        [SerializeField]
        private List<MonoBehaviour> activeStateBehaviours = new List<MonoBehaviour>();

        private void OnEnable() => GameState.OnStateChange += StateChanged;
        private void OnDisable() => GameState.OnStateChange -= StateChanged;

        private void StateChanged(object sender, GameState.State state)
        {
            SetBehaviourActive(state == GameState.State.Active);
        }

        private void SetBehaviourActive(bool isEnable)
        {
            foreach(var behaviour in activeStateBehaviours)
                behaviour.enabled = isEnable;
        }
    }
}