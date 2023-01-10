using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classic
{
    public class LoadScene : MonoBehaviour
    {
        public string sceneName;
        public void LoadGameScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
