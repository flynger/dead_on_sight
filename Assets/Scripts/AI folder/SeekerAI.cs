using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StarterAssets {
    public class SeekerAI : AIScript
    {
        public float killTime;
        

        public override void BeginPlayerKill()
        {
            StartCoroutine(KillPlayer());
        }

        public override void LostPlayer()
        {
            StopCoroutine(KillPlayer());
        }

        IEnumerator KillPlayer()
        {
            yield return new WaitForSeconds(killTime);
            playerRef.GetComponent<Entity>().ApplyDamage(GetComponent<Entity>().baseDamage);
        }
    }

}