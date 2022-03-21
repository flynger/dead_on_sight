using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class Terminal : Entity
    {
        public bool canUseHack = true;
        public GameObject target;
/*
        public int hitPoints
        {
            get;
            set;
        }

        public int baseDamage
        {
            get;
        }

        public bool isHackable
        {
            get;
        }*/

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            HackCheck();
        }

        public void HackCheck()
        {
            // Debug.DrawRay(transform.position + new Vector3(0.06f, 1.6f, 0f), transform.TransformDirection(Vector3.forward) * 16, Color.green);
            // if (canUseHack && controller._input.possess)
            // {
            //     RaycastHit hit;
            //     //ToggleCollider(false);
            //     if (Physics.Raycast(transform.position + new Vector3(0.06f, 1.6f, 0f), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, gameManager.entityLayer))
            //     {
            //         Debug.Log(hit.collider.gameObject);
            //         Hack(hit.collider.gameObject);
            //         StartCoroutine(HackCooldown());
            //     }
            //     else
            //     {
            //         //Debug.Log("hack fail");
            //     }
            //     //ToggleCollider(true);
            // }
            GameObject obj;
            if (canUseHack && controller._input.possess && CheckVision(16, out obj) && obj != null && obj.CompareTag("enemy"))
            {
                Hack(obj);
                StartCoroutine(HackCooldown());
            }
        }

        IEnumerator HackCooldown()
        {
            canUseHack = false;
            yield return new WaitForSeconds(5f);
            canUseHack = true;
            gameManager.SelectNewPlayer(gameObject);
        }

        void Hack(GameObject obj)
        {
            gameManager.SelectNewPlayer(obj);
        }

        void ToggleCollider(bool state)
        {
            gameObject.GetComponent<Collider>().enabled = state;
        }
    }
}
