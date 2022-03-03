using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript: MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;

    //patroll
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //states
    public float sightRange;
    public bool inSightRange;

    private void Awake()
    {

        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        RaycastHit hit;
        //check for player
        inSightRange = Physics.SphereCast(transform.position, sightRange, transform.forward, out hit, playerMask);

        if (!inSightRange)
        {
            Patrol();
        }
        else
        {
            DiscoverPlayer();
        }
    }
    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void DiscoverPlayer()
    {
        Debug.Log("AI found the player");
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
        {
            Debug.Log("setwalkpoint");
            walkPointSet = true;
        }
    }

}
