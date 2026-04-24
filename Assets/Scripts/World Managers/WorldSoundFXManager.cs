using UnityEngine;

namespace MySoulsProject
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager Singleton;

        [Header("Action Sounds")]
        public AudioClip rollSFX;

        private void Awake()
        {
            if (Singleton == null)
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
    }
}

