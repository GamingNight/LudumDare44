using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCarNavigator : MonoBehaviour
{
    public GameObject targetContainer;
    public bool fearStatus = false;
    private List<Vector3> targets;
    private Vector3 currentTarget;
    //private NavMeshAgent agent;
    private int nbPoints;
    private int currentID;
    public int posID = 0;

    // Start is called before the first frame update
    void Start() {
        //agent = GetComponent<NavMeshAgent>();
        targets = new List<Vector3>();
        foreach (Transform t in targetContainer.transform) {
            targets.Add(t.transform.position);
        }
        int nbPoints = targetContainer.transform.childCount;
        UpdateTarget();
    }
        

    private void Update() {
        GameObject player = GameManager.Instance().GetPlayer();
        CarController car_controller = player.GetComponent<CarController>();
        Vector3 distance = (player.transform.position) - transform.position;
        bool isNearPlayer = (distance.magnitude < 30);

        if (car_controller.getFearStatus() && !fearStatus && isNearPlayer) {
            //fearStatus = true;
            //agent.speed = 5 * agent.speed;
        }
        else if (!car_controller.getFearStatus() && fearStatus)
        {
            //fearStatus = false;
            //agent.speed = agent.speed / 5;
        }

        //if ((agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 1) || agent.pathStatus == NavMeshPathStatus.PathInvalid) {
        //    UpdateTarget();
        //}

        //animator.SetBool("walking", agent.velocity.magnitude > 0.01);
    }

    private void UpdateTarget() {
        currentID++;
        if (currentID == nbPoints)
            currentID = 0;
        currentTarget = targets[currentID];
        //agent.SetDestination(currentTarget);
    }
}
