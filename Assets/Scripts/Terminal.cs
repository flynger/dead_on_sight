using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public bool fingerOffInteract = true;

        void Start()
        {

        }

        void Update()
        {
            UpdateTarget();
        }

        public override bool ApplyDamage(int damage)
        {
            
            if (base.ApplyDamage(damage))
            {
                LossScreen.SetActive(true);
                StartCoroutine(RestartGame(5f));
                return true;
            } else if (hitPoints <= 5)
            {
                halfHPIndicator.SetActive(true);
            }
            return false;
        }

        public void UpdateTarget()
        {
            if (!hasAI)
            {
                if (CheckVision(16, out target) && target != null)
                {
                    HackCheck();
                    InteractCheck();
                }
            }
        }

        public void HackCheck()
        {
            if (target.CompareTag("enemy") && controller._input.possess && canUseHack)
            {
                Hack(target);
                StartCoroutine(HackCooldown());
            }
        }

        public void InteractCheck()
        {
            if (Vector3.Distance(transform.position + new Vector3(0.06f, 1.6f, 0f), target.transform.position) <= 4)
            {
                if (controller._input.action)
                {
                    if (target.CompareTag("interactable") && fingerOffInteract)
                    {
                        target.GetComponent<Activate>().activateEffect();
                        //StartCoroutine(InteractCooldown());
                    }
                    fingerOffInteract = false;
                }
                else
                {
                    fingerOffInteract = true;
                }
            }
        }

        IEnumerator HackCooldown()
        {
            canUseHack = false;
            yield return new WaitForSeconds(5f);
            canUseHack = true;
            gameManager.SelectNewPlayer(gameObject);
        }

        public IEnumerator RestartGame(float time)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
         

        // IEnumerator InteractCooldown()
        // {
        //     fingerOffInteract = false;
        //     yield return new WaitForSeconds(1f);
        //     fingerOffInteract = true;
        // }

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
