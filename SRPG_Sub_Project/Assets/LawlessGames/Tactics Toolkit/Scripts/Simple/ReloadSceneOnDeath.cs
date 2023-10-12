using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TacticsToolkit
{
    public class ReloadSceneOnDeath : MonoBehaviour
    {
        public CharacterManager characterManager;

        // Update is called once per frame
        void Update()
        {
            if (!characterManager.isAlive)
                LoadScene();
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
