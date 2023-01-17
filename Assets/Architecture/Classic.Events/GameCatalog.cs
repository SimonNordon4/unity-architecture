using UnityEngine;

namespace Architecture.Classic.Events
{
    
    /// <summary>
    /// Game Catalog is an example of the Service Locator pattern and is a Singleton.
    /// It allows us a single access point to all game required objects in the scene.
    /// </summary>
    public class GameCatalog : MonoBehaviour
    {
        private static GameCatalog _instance;
        public static GameCatalog Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameCatalog>();
                }

                return _instance;
            }
        }
        
        [field: SerializeField] public GameInterface GameInterface { get; private set; }
        [field: SerializeField] public GameState GameState { get; private set; }
        [field: SerializeField] public GameObject Player { get; private set; }
        [field: SerializeField] public EnemySpawner EnemySpawner { get; private set; }
        
        [field: SerializeField] public GameUI UI { get; private set; }
        [field: SerializeField] public ApplicationInterface App { get; private set; }

    }
}