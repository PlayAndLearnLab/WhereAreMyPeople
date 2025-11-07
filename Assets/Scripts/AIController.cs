using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    public float visionRange;

    void Start()
    {
        if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
    }

    void Update()
    {
        animator.SetFloat("ForwardSpeed", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        Debug.Log(navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        //print(navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        //print(transform.position);
        //print(player.transform.position);
        //print(Vector3.Distance(transform.position, player.transform.position));
        if (Vector3.Distance(transform.position, player.transform.position) > visionRange) return;
        //print("Robot avance");
        navMeshAgent.SetDestination(player.transform.position);
    }
}
