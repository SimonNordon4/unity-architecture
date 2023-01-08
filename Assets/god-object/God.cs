using System;

namespace GodObject{
    public class God : UnityEngine.MonoBehaviour
    {
        public void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

