using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavigation : MonoBehaviour {

    public GameObject targetContainer;
    public Animator animator;
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
        if ((agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 1) || agent.pathStatus == NavMeshPathStatus.PathInvalid) {
            UpdateTarget();
        }
        animator.SetBool("walking", agent.velocity.magnitude > 0.01);
    }

    private void UpdateTarget() {
        currentTarget = targets[UnityEngine.Random.Range(0, targets.Count)];
        agent.SetDestination(currentTarget);
    }
}
