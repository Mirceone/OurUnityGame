using UnityEngine;
using Unity.Netcode;

namespace MySoulsProject
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Singleton;
        
        [Header("NETWORK JOIN")]
        [SerializeField] bool startGameAsClient;
        
        [HideInInspector] public PlayerUIHudManager playerUIHudManager;

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
            
            playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
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
