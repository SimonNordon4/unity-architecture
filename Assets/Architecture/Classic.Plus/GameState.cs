using UnityEngine;

namespace Architecture.Classic.Plus
{
    public class GameState : MonoBehaviour
    {
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
        }
    }
}