using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StarterAssets
{
    public class SwarmieAI : AIScript
    {
        private IEnumerator killCoroutine;
        public bool trackingPlayer;
        private float moveSpeed;
        private float turnSpeed;
        void Awake()
        {

            moveSpeed = agent.speed;
            turnSpeed = agent.angularSpeed;
        }

        public override void BeginPlayerKill()
        {
            if (!trackingPlayer)
            {
                agent.SetDestination(transform.position);
                trackingPlayer = true;
                agent.speed = 16;
                killCoroutine = FollowPlayer();
                StartCoroutine(killCoroutine);
            }
            
        }

        public override void LostPlayer()
        {
            if (trackingPlayer)
            {
                gameManager.warningSign.SetActive(false);
                agent.speed = moveSpeed;
                agent.angularSpeed = turnSpeed;
                StopCoroutine(killCoroutine);
                trackingPlayer = false;
            }

        }

        IEnumerator FollowPlayer()
        {
            WaitForSeconds wait = new WaitForSeconds(.4f);
            while (true)
            {
                yield return wait;
                agent.SetDestination(gameManager.player.transform.position);
                gameManager.warningSign.SetActive(true);
                //if within 1 tile, deal damage
                Collider[] rangeCheck = Physics.OverlapSphere(transform.position, 1, playerMask);
                if (rangeCheck.Length != 0)
                {
                    Debug.Log("SW delt damage");
                    gameManager.player.GetComponent<Entity>().ApplyDamage(GetComponent<Entity>().baseDamage);
                }
            }
            
        }

        


    }

}


