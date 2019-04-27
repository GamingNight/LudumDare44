using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavigation : MonoBehaviour {

    public GameObject targetContainer;
    private List<Vector3> targets;
    private Vector3 currentTarget;
    private NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        targets = new List<Vector3>();
        foreach (Transform t in targetContainer.transform) {
            targets.Add(t.transform.position);
        }
        UpdateTarget();
    }

    private void Update() {
        if ((agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) || agent.pathStatus == NavMeshPathStatus.PathInvalid) {
            UpdateTarget();
        }
    }

    private void UpdateTarget() {
        currentTarget = targets[UnityEngine.Random.Range(0, targets.Count)];
        agent.SetDestination(currentTarget);
    }
}
