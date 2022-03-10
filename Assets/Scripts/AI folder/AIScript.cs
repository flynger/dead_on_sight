using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript: MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject playerRef;
    public LayerMask groundMask, playerMask;

    //patroll
    public GameObject[] walkPointArray;
    public int walkPointIndex;
    public bool walkPointSet;
    public float walkPointRange;
    public bool canPatrol;

    //states
    public float sightRange;
    [Range(0,360)]
    public float sightAngle;
    public bool inSightRange;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
                
        if (inSightRange)
        {
            DiscoverPlayer();
        }
        
        else
        {
            Patrol();
        }
        
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
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, sightRange, playerMask);

        if (rangeCheck.Length != 0)
        {
            
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2)
            {
                
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position + new Vector3(0, 1.6f, 0), directionToTarget, distanceToTarget, groundMask))
                {
                    
                    inSightRange = true;
                    
                }
                else { inSightRange = false; }
            }
            else { inSightRange = false; }
        }
        else if (inSightRange) { inSightRange = false; }
    }
    private void Patrol()
    {
        if (canPatrol)
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet) agent.SetDestination(walkPointArray[walkPointIndex].transform.position);

            Vector3 distanceToWalkPoint = transform.position - walkPointArray[walkPointIndex].transform.position;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }
        
    }

    
    
    private void DiscoverPlayer()
    {
        
        canPatrol = false;
        agent.SetDestination(transform.position);
    }
    
    private void SearchWalkPoint()
    {
        walkPointIndex++;
        if (walkPointIndex >= walkPointArray.Length)
        {
            walkPointIndex = 0;
        }
        walkPointSet = true;

        
    }

    

    
}
