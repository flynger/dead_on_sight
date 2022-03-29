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
                agent.speed = moveSpeed;
                agent.angularSpeed = turnSpeed;
                StopCoroutine(killCoroutine);
                alreadyStartedKP = false;
            }
            
        }

        IEnumerator KillPlayer()
        {
            agent.speed = .5f;
            agent.angularSpeed = 2000f;
            WaitForSeconds wait = new WaitForSeconds(.2f);
            float i = 0;
            while (i < killTime - 2f)
            {
                yield return wait;
                i += .2f;
                agent.SetDestination(playerRef.transform.position);
            }

            while (i < killTime)
            {
                yield return wait;
                i += .2f;
                agent.SetDestination(playerRef.transform.position);
                playerRef.GetComponent<Entity>().ApplyDamage(GetComponent<Entity>().baseDamage);
            }


        }
    }

}