using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [Space]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [Space]
    private bool pathfind = true;

    public Transform PlayerTransform { get => playerTransform; set => playerTransform = value; }
    public NavMeshAgent NavMeshAgent { get => navMeshAgent; }
    public bool Pathfind { get => pathfind; set => pathfind = value; }

    private void Start() {
        UpdatePlayerTransform();
    }

    private void UpdatePlayerTransform() { playerTransform = GameObject.FindGameObjectWithTag("Player").transform; }

    private void Update() {
        if (navMeshAgent.isOnNavMesh) {
            navMeshAgent.isStopped = !pathfind;
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }
}
