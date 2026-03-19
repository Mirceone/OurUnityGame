using UnityEngine;
using Unity.Netcode;

namespace MySoulsProject
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Singleton;
        [Header("NETWORK JOIN")]
        [SerializeField] bool startGameAsClient;

        private void Awake()
        {
            if(Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if(startGameAsClient)
            {
                startGameAsClient = false;
                // WE MUST FISRT SHUT DOWN, BECAUSE WE HAVE STARTED AS HOST DURING THE TITLE SCREEN
                NetworkManager.Singleton.Shutdown();
                // WE THEN RESTART, AS A CLIENT
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
