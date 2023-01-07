using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classic
{
    public class LoadGame : MonoBehaviour
    {
        public void LoadGameScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("classic-prototype/scenes/classic-prototype-game");
        }
    }
}

