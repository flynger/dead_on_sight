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
            if (canUseHack && controller._input.possess)
            {
                RaycastHit hit;
                ToggleCollider(false);
                if (Physics.Raycast(transform.position + new Vector3(0, 0.8f, 0), Vector3.forward, out hit))
                {
                    Debug.Log(hit.collider.gameObject);
                    if (hit.collider.gameObject.GetComponent<Entity>())
                        Hack(hit.collider.gameObject);
                }
                ToggleCollider(true);
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
