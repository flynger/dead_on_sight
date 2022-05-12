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
            while (i < killTime)
            {
                
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0,gameManager.player.transform.position.y,0) - new Vector3(0,transform.position.y,0));
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