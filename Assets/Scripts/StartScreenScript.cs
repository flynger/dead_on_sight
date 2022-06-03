using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarterAssets
{
    public class StartScreenScript : MonoBehaviour
    {
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("1"))
            {
                GoToScene(1);
            }
        }

        public void GoToScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

}
