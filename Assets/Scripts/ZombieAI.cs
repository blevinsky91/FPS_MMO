using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class ZombieAI : MonoBehaviour
{
    public enum WanderType { Random, Waypoint};

    public FirstPersonController fpsc;
    public WanderType wanderType = WanderType.Random;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public float fov = 120f;
    public float viewDistance = 10f;
    public float wanderRadius = 7f;
    public Transform[] waypoints; 

    private bool isAware = false;
    private Vector3 wanderPoint;
    private NavMeshAgent agent;
    private Renderer renderer;
    private int wayPointIndex = 0;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        animator = GetComponentInChildren<Animator>();
        wanderPoint = RandomWanderPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAware)
        {
            agent.SetDestination(fpsc.transform.position);
            animator.SetBool("Aware", true);
            agent.speed = chaseSpeed;
        }
        else
        {
            SearchForPlayer();
            Wander();
            animator.SetBool("Aware", false);
            agent.speed = wanderSpeed;
        }
    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(fpsc.transform.position)) < fov / 2f)
        {
            if (Vector3.Distance(fpsc.transform.position, transform.position) < viewDistance)
            {
                RayCastHit hit;
                if (Physics.Linecast(transform.position, fpsc.transfomr.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }
                }
            }
        }
    }

    public void OnAware()
    {
        isAware = true;
    }

    public void Wander()
    {
        if (Vector3.Distance(transform.position, wanderPoint) < 0.5f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (RandomWanderPoint().InsideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }
}
 