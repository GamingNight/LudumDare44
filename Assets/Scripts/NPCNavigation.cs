using System.Collections.Generic;
using UnityEngine;

public class NPCNavigation : MonoBehaviour {

    public GameObject targetContainer;
    private List<Vector3> targets;
    private Vector3 currentTarget;

    void Start() {
        targets = new List<Vector3>();
        foreach (Transform t in targetContainer.transform) {
            targets.Add(t.transform.position);
        }
        UpdateTarget();
    }

    private void UpdateTarget() {
        currentTarget = targets[UnityEngine.Random.Range(0, targets.Count)];
    }
}
