using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class Terminal : Entity
    {
        public bool canUseHack = true;
        public GameObject target;
        public Inventory inventory;
        public GameObject pickupRange;
        public GameObject[] itemsWithinRange;
        public GameObject halfHPIndicator;
        public GameObject LossScreen;

        void Start()
        {

        }

        void Update()
        {
            HackCheck();
        }

        public override bool ApplyDamage(int damage)
        {
            
            if (base.ApplyDamage(damage))
            {
                LossScreen.SetActive(true);
                return true;
            } else if (hitPoints <= 5)
            {
                halfHPIndicator.SetActive(true);
            }
            return false;
        }

        public void HackCheck()
        {
            if (CheckVision(16, out target) && target != null)
            {
                if (target.CompareTag("enemy") && controller._input.possess && canUseHack)
                {
                    Hack(target);
                    StartCoroutine(HackCooldown());
                }
                // else if (target.CompareTag("item") && controller._input.interact)
                // {
                //     inventory.DropItem();
                //     inventory.item = target;
                // }
            }
            else target = null;
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
