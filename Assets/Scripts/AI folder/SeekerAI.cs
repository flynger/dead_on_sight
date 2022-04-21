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
            while (i < killTime - 4f)
            {
                gameManager.warningSign.SetActive(true);
                yield return wait;
                i += .4f;
                agent.SetDestination(gameManager.player.transform.position);
            }

            while (i < killTime)
            {
                gameManager.warningSign.SetActive(true);
                yield return wait;
                i += .4f;
                agent.SetDestination(gameManager.player.transform.position);
                gameManager.player.GetComponent<Entity>().ApplyDamage(GetComponent<Entity>().baseDamage);
            }


        }
    }

}