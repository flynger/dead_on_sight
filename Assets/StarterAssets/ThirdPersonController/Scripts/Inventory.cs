using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace StarterAssets
{
    public class Inventory : MonoBehaviour
    {
        public GameObject item;
        public GameObject itemRoot;
        private bool eBuffer;
        // public GameObject camera;
        // public GameObject player;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*        Vector3 camPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, player.transform.position.z - 2);
                    Quaternion camRotation = camera.transform.rotation;
                    inventory.transform.position = camPos;
                    inventory.transform.rotation = camRotation;*/
            ItemCheck();
            if (item != null)
            {
                item.transform.position = itemRoot.transform.position;
                item.transform.rotation = itemRoot.transform.rotation;
            }
        }

        public void DropItem()
        {
            if (item != null)
            {
                item.GetComponent<Collider>().enabled = true;
                item.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        public void PickupItem(GameObject obj)
        {
            item = obj;
            item.GetComponent<Collider>().enabled = false;
            item.GetComponent<Rigidbody>().useGravity = false;
        }

        public void ItemCheck() {
            Terminal parent = gameObject.GetComponentInParent<Terminal>();
            if (parent != null && parent.target != null && parent.target.CompareTag("item"))
            {
                if (!eBuffer && parent.DistanceTo(parent.target) <= parent.pickupRange && parent.controller._input.action)
                {
                    Debug.Log("PICKING UP ITEM");
                    DropItem();
                    PickupItem(parent.target);
                    eBuffer = true;
                }
                else if (!parent.controller._input.action) {
                    eBuffer = false;
                }
            }
        }
    }
}