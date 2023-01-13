using UnityEngine;

namespace Architecture.Classic.Plus
{
    /// <summary>
    /// In this example we've transformed the GameInterface into a Singleton. Making it globally accessible.
    /// It's generally okay to make a Singleton of an object when there's only ever meant to be a single instance.
    /// We could also have made the Player a Singleton as well, however if have other objects access the Player
    /// through the Game Manager Singleton, it means we have a central source of global level state.
    /// </summary>
    public class GameInterface : MonoBehaviour
    {
        private GameState _state;
        private GameObject _winScreen;
        private GameObject _loseScreen;
        private void Start()
        {
            _state = GameCatalog.Instance.GameState;
            _winScreen = GameCatalog.Instance.WinScreenUI;
            _loseScreen = GameCatalog.Instance.LoseScreenUI;
        }

        public void StartGame()
        {
            _state.SetState(GameState.State.Active);
        }

        public void ResumeGame()
        {
            _state.SetState(GameState.State.Active);
        }
        
        public void PauseGame()
        {
            _state.SetState(GameState.State.Paused);
        }

        public void WinGame()
        {
            _state.SetState(GameState.State.Finished);
            _winScreen.SetActive(true);
        }

        public void LoseGame()
        {
            _state.SetState(GameState.State.Finished);
            _loseScreen.SetActive(false);
        }
    }
}