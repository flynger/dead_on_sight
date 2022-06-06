using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarterAssets
{
    public class StartScreenScript : MonoBehaviour
    {

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // Update is called once per frame
        void Update()
        {
            
        }

        public void GoToScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

}
