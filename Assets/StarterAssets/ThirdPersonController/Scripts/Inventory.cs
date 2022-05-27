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

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
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
                Debug.Log("Dropping current ITEM");
                item.GetComponent<Collider>().enabled = true;
                item.GetComponent<Rigidbody>().useGravity = true;
                item = null;
            }
        }

        public void PickupItem(GameObject obj)
        {
            Debug.Log("Picking up new ITEM");
            item = obj;
            item.GetComponent<Collider>().enabled = false;
            item.GetComponent<Rigidbody>().useGravity = false;
        }

        public void ItemCheck() {
            Terminal parent = gameObject.GetComponent<Terminal>();
            // if parent Terminal is active
            if (parent != null && GetComponent<ThirdPersonController>().enabled)
            {
                // drop item
                if (parent.controller._input.drop)
                {
                    DropItem();
                }
                // check if valid item in view
                if (parent.target != null && parent.target.CompareTag("item")) {
                    // check for buffer and item pickup range
                    if (!eBuffer && parent.DistanceTo(parent.target) <= parent.pickupRange)
                    {
                        if (parent.controller && parent.controller._input.action)
                        {
                            DropItem();
                            PickupItem(parent.target);
                            eBuffer = true;
                        }
                    }
                    // reset buffer (buffer prevents holding down and swapping between two items infinitely)
                    else if (!parent.controller._input.action) {
                        eBuffer = false;
                    }
                }
            }
        }
    }
}