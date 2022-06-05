using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public LayerMask playerLayer;
        public GameObject HPLossIndicator, HPLossIndicator1, lossScreen, crossHair, grabIndicator, hackIndicator, interIndicator, warningSign;
        public Slider runSlider;

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
            player.GetComponent<Entity>().ToggleAI(true);
            player.gameObject.layer = LayerMask.NameToLayer("Entity");
            player.gameObject.tag = "enemy";
            player = obj;
            Entity entity = player.GetComponent<Entity>();
            // if (entity is Terminal)
            // {
            //     Debug.Log("possessed another Terminal!");
            // }
            player.GetComponent<Entity>().ToggleInput(true);
            player.GetComponent<Entity>().ToggleAI(false);
            //player.GetComponent<Terminal>().canUseHack = true;
            player.gameObject.layer = LayerMask.NameToLayer("Player");
            player.gameObject.tag = "player";
            virtualCam.Follow = player.transform.Find("PlayerCameraRoot");
            virtualCam.LookAt = player.transform.Find("PlayerCameraRoot");
        }
    }
}
