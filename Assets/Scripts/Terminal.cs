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
                gameManager.lossScreen.SetActive(true);
                gameManager.crossHair.SetActive(false);
                StartCoroutine(RestartGame(5f));
                return true;
            } else {
                UpdateUI();
                return false;
            }
            
        }

        public void UpdateUI()
        {
            if (hitPoints <= 3)
            {
                gameManager.HPLossIndicator.SetActive(true);
            }
            else if (hitPoints <= 6)
            {
                gameManager.HPLossIndicator1.SetActive(true);
            }
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
                //StartCoroutine(HackCooldown());
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

        public bool CheckVision(float distance, out GameObject obj)
        {
            // account for starting point based on entity
            //Debug.DrawRay(controller._mainCamera.transform.position, controller._mainCamera.transform.TransformDirection(Vector3.forward) * distance, Color.green);
            RaycastHit hit;
            //bool result = Physics.Raycast(transform.position + new Vector3(0.06f, 1.6f, 0f), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
            bool result = Physics.Raycast(controller._mainCamera.transform.position, controller._mainCamera.transform.TransformDirection(Vector3.forward), out hit, distance);
            if (result) obj = hit.collider.gameObject;
            else obj = null;
            return result;
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
            Debug.Log(obj);
            gameManager.SelectNewPlayer(obj);
            isHackable = false;
        }

        void ToggleCollider(bool state)
        {
            gameObject.GetComponent<Collider>().enabled = state;
        }

        public float DistanceTo(GameObject other) {
            return Vector3.Distance(transform.position + new Vector3(0.06f, 1.6f, 0f), other.transform.position);
        }
    }
}
