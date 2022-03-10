using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
<<<<<<< HEAD
<<<<<<< HEAD
    public class Terminal : MonoBehaviour, Entity
    {
        public GameManager gameManager;
        public ThirdPersonController controller;
        public bool canHack = true;
        public GameObject target;

=======
=======
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
    public class Terminal : Entity
    {
        public bool canUseHack = true;
        public GameObject target;
/*
<<<<<<< HEAD
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
=======
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
        public int hitPoints
        {
            get;
            set;
        }
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
=======

>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
        public int baseDamage
        {
            get;
        }

        public bool isHackable
        {
            get;
<<<<<<< HEAD
<<<<<<< HEAD
        }

        void Start()
        {

=======
=======
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
        }*/

        void Start()
        {
            
<<<<<<< HEAD
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
=======
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
        }

        // Update is called once per frame
        void Update()
        {
<<<<<<< HEAD
<<<<<<< HEAD
            if (canHack && controller._input.possess)
            {
                Hack(target);
                StartCoroutine(HackCooldown());
            }
        }

        public void ApplyDamage(int damage)
        {
            hitPoints -= damage;
        }

        public void ToggleInput(bool state)
        {
            controller._rawInput.enabled = state;
        }

        IEnumerator HackCooldown()
        {
            canHack = false;
            yield return new WaitForSeconds(5f);
            canHack = true;
=======
=======
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
            HackCheck();
        }

        public void HackCheck()
        {
            Debug.DrawRay(transform.position + new Vector3(0.06f, 1.6f, 0f), transform.TransformDirection(Vector3.forward) * 16, Color.green);
            if (canUseHack && controller._input.possess)
            {
                RaycastHit hit;
                //ToggleCollider(false);
                if (Physics.Raycast(transform.position + new Vector3(0.06f, 1.6f, 0f), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, gameManager.entityLayer))
                {
                    Debug.Log(hit.collider.gameObject);
                    Hack(hit.collider.gameObject);
                    StartCoroutine(HackCooldown());
                }
                else
                {
                    //Debug.Log("hack fail");
                }
                //ToggleCollider(true);
            }
        }

        IEnumerator HackCooldown()
        {
            canUseHack = false;
            yield return new WaitForSeconds(5f);
            canUseHack = true;
<<<<<<< HEAD
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
=======
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
            gameManager.SelectNewPlayer(gameObject);
        }

        void Hack(GameObject obj)
        {
            gameManager.SelectNewPlayer(obj);
        }
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96

        void ToggleCollider(bool state)
        {
            gameObject.GetComponent<Collider>().enabled = state;
        }
<<<<<<< HEAD
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
=======
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
    }
}
