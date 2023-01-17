using System;
using UnityEngine;

namespace Architecture.Classic.Events
{
    public class GameState : MonoBehaviour
    {
        public event EventHandler<State> OnStateChange;
        public enum State
        {
            None = 0,
            Active = 1,
            Paused = 2,
            Finished = 3
        }
        
        [field:SerializeField]
        public State CurrentState { get; private set; } = State.None;
        
        public void SetState(State state)
        {
           CurrentState = state;
           OnStateChange?.Invoke(this,state);
        }
    }
}