using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class Terminal : MonoBehaviour, Entity
    {
        public GameManager gameManager;
        public ThirdPersonController controller;
        public bool canHack = true;
        public GameObject target;

        public int hitPoints
        {
            get;
            set;
        }
        public int baseDamage
        {
            get;
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
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

        IEnumerator HackCooldown()
        {
            canHack = false;
            yield return new WaitForSeconds(5f);
            canHack = true;
        }

        void Hack(GameObject obj)
        {
            gameManager.SelectNewPlayer(obj);
        }
    }
}
