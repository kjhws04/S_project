using UnityEngine;
using UnityEngine.SceneManagement;

namespace TacticsToolkit

{
    public class SceneSwitcher : MonoBehaviour
    {
        private int sceneIndex;
        // Start is called before the first frame update
        void Start()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }


        public void LoadNextScene()
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }

        public void LoadPreviousScene()
        {
            SceneManager.LoadScene(sceneIndex - 1);
        }
    }
}