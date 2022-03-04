using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class GameManager : MonoBehaviour
    {
        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectNewPlayer(GameObject obj)
        {
            player.GetComponent<Entity>().ToggleInput(false);
            player = obj;
            Entity entity = player.GetComponent<Entity>();
            if (entity is Terminal)
            {
                Debug.Log("possessed another Terminal!");
            }
            player.GetComponent<Entity>().ToggleInput(true);
        }
    }
}
