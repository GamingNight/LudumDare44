using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject objectToFollow;

    void Update() {

        transform.position = new Vector3(objectToFollow.transform.position.x, transform.position.y, objectToFollow.transform.position.z);
    }
}
