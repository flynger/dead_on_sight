using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StarterAssets
{
    
    public class SwarmieSoundDetectAI : MonoBehaviour
    {
        public NavMeshAgent agent;
        public LayerMask groundMask, soundMask;

        public float sightRange;
        [Range(0, 360)]
        public float sightAngle;
        public bool inSightRange;

        void Start()
        {
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(.2f);

            while (true) //bool should be false when player dies
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeCheck = Physics.OverlapSphere(transform.position, sightRange, soundMask);

            if (rangeCheck.Length != 0)
            {

                Transform target = rangeCheck[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2)
                {

                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position + new Vector3(0, .5f, 0), directionToTarget, distanceToTarget, groundMask))
                    {

                        inSightRange = true;
                        GoToSoundPoint(target.transform.position);
                    }
                    else { }
                }
                else {  }
            }
            else if (inSightRange)
            {
                
            }
        }

        public void GoToSoundPoint(Vector3 pos)
        {
            agent.SetDestination(pos);
        }
    }
}
