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
                agent.speed = 6;
                agent.angularSpeed = 120;
                StopCoroutine(killCoroutine);
                alreadyStartedKP = false;
            }
            
        }

        IEnumerator KillPlayer()
        {
            agent.speed = .1f;
            agent.angularSpeed = 1000f;
            WaitForSeconds wait = new WaitForSeconds(.2f);
            float i = 0;
            while (i < killTime - 1)
            {
                yield return wait;
                i += .2f;
                agent.SetDestination(playerRef.transform.position);
            }

            while (i < killTime)
            {
                i += .2f;
                agent.SetDestination(playerRef.transform.position);
                playerRef.GetComponent<Entity>().ApplyDamage(GetComponent<Entity>().baseDamage);
            }


        }
    }

}