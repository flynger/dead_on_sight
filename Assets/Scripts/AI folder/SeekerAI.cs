using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StarterAssets 
{
    public class SeekerAI : AIScript
    {
        public float killTime;
        public bool alreadyStartedKP;
        private IEnumerator killCoroutine;
        private float moveSpeed;
        private float turnSpeed;
        public GameObject sightLight;
        public GameObject sightLightReturnPoint;
        
        void Awake()
        {
            
            moveSpeed = agent.speed;
            turnSpeed = agent.angularSpeed;
        }
        public override void BeginPlayerKill()
        {

            if (!alreadyStartedKP)
            {
                
                agent.SetDestination(transform.position);
                killCoroutine = KillPlayer();
                StartCoroutine(killCoroutine);
                alreadyStartedKP = true;
            }
            
        }

        public override void LostPlayer()
        {
            if (alreadyStartedKP)
            {
                Quaternion lookRotation = Quaternion.LookRotation(sightLightReturnPoint.transform.position - sightLight.transform.position);
                sightLight.transform.rotation = Quaternion.Lerp(sightLight.transform.rotation, lookRotation, .5f);
                gameManager.warningSign.SetActive(false);
                agent.speed = moveSpeed;
                agent.angularSpeed = turnSpeed;
                StopCoroutine(killCoroutine);
                alreadyStartedKP = false;
            }
            
        }

        IEnumerator KillPlayer()
        {
            agent.speed = 1f;
            //agent.angularSpeed = 8000f;
            WaitForSeconds wait = new WaitForSeconds(.4f);
            float i = 0;
            while (i < killTime)
            {
                
                Quaternion lookRotation = Quaternion.LookRotation(gameManager.player.transform.position - sightLight.transform.position);
                lookRotation.eulerAngles = new Vector3(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.z);
                gameManager.warningSign.SetActive(true);
                yield return wait;
                i += .2f;
                sightLight.transform.rotation = Quaternion.Lerp(sightLight.transform.rotation, lookRotation , .5f);
                agent.SetDestination(gameManager.player.transform.position);
                if (i >= 4)
                {
                    gameManager.player.GetComponent<Entity>().ApplyDamage(GetComponent<Entity>().baseDamage);
                }
            }
        }
    }

}