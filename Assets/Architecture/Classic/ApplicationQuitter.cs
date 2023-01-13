using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Classic
{
    public class ApplicationQuitter : MonoBehaviour
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}

