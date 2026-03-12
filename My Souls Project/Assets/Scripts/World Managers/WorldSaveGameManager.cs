using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MySoulsProject
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager Singleton;

        [SerializeField] int worldSceneIndex = 1;

        private void Awake()
        {
            // ONLY ONE INSTANCE OF THE WORLD SAVE GAME MANAGER AT ANY SINGLE TIME
            if (Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            yield return null;
        }

        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}