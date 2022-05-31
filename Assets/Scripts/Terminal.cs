using System.Runtime;
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
        public float pickupRange;
        //public GameObject[] itemsWithinRange;
        public bool fingerOffInteract = true;
        public bool itemInRange, enemyInRange, interactInRange;

        void Start()
        {
            //StartCoroutine(RelevancyDetection());
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
            else
            {
                gameManager.HPLossIndicator.SetActive(false);
            }
            if (hitPoints <= 6)
            {
                gameManager.HPLossIndicator1.SetActive(true);
            }
            else
            {
                gameManager.HPLossIndicator1.SetActive(false);
            }
        }

        
        public void UpdateTarget()
        {
            if (!isAIEnabled())
            {
                if (CheckVision(16, out target) && target != null)
                {
                    HackCheck();
                    InteractCheck();
                    UpdateIndicators();
                }
            }
        }

        void UpdateIndicators()
        {
            if (target.CompareTag("item") && DistanceTo(target) <= pickupRange)
            {
                
                gameManager.grabIndicator.SetActive(true);
            }
            else
            {
                gameManager.grabIndicator.SetActive(false);
            }

            if (enemyInRange && target)
            {
                
                gameManager.hackIndicator.SetActive(true);
            }
            else
            {
                gameManager.hackIndicator.SetActive(false);
            }

            if (interactInRange)
            {
                
                gameManager.interIndicator.SetActive(true);
            }
            else
            {
                gameManager.interIndicator.SetActive(false);
            }
        }

        public void HackCheck()
        {
            if (target.CompareTag("enemy") && canUseHack && target.GetComponent<Entity>().isHackable)
            {
                enemyInRange = true;
                if (controller._input.possess)
                {
                    Hack(target);
                }
                //StartCoroutine(HackCooldown());
            }
            else enemyInRange = false;
        }

        public void InteractCheck()
        {
            if (target.CompareTag("interactable") && Vector3.Distance(transform.position + new Vector3(0.06f, 1.6f, 0f), target.transform.position) <= 4)
            {
                interactInRange = true;
                if (controller._input.action)
                {
                    if (fingerOffInteract)
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
            else interactInRange = false;
        }

        public bool CheckVision(float distance, out GameObject obj)
        {
            // account for starting point based on entity
            //Debug.DrawRay(controller._mainCamera.transform.position, controller._mainCamera.transform.TransformDirection(Vector3.forward) * distance, Color.green);
            RaycastHit hit;
            //bool result = Physics.Raycast(transform.position + new Vector3(0.06f, 1.6f, 0f), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
            bool result = Physics.Raycast(controller._mainCamera.transform.position, controller._mainCamera.transform.TransformDirection(Vector3.forward), out hit, distance, ~gameManager.playerLayer);
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
            UpdateUI();
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
