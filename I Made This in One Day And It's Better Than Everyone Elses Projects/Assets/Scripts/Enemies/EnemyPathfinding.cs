using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour {
    [SerializeField] public Transform playerTransform;
    [Space]
    [SerializeField] public NavMeshAgent navMeshAgent;
    [Space]
    public bool pathfind = true;

    private void Start() {
        UpdatePlayerTransform();
    }

    private void UpdatePlayerTransform() { playerTransform = GameObject.FindGameObjectWithTag("Player").transform; }

    private void Update() {
        navMeshAgent.isStopped = !pathfind;
        navMeshAgent.SetDestination(playerTransform.position);
    }
}
