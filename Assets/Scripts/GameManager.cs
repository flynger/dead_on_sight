using Cinemachine;
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
        public GameObject player;
        public CinemachineVirtualCamera virtualCam;
        public LayerMask entityLayer;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player");
            virtualCam = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
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
            virtualCam.Follow = player.transform.Find("PlayerCameraRoot");
        }
    }
}
